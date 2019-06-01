namespace Core.Services.Accounts
{
	using AutoMapper;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Security.Claims;

	using Core.Services.Claims;
	using CoreClaimTypes = Core.Services.Claims.ClaimTypes;
	using Core = Core.Models;
	using Data = Infrastructure.Entities;
	using Infrastructure.Repositories;
    using Utils;

    public class AccountService<TUser, TRole, TKey> : IAccountService<TUser, TRole, TKey>
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TKey : IEquatable<TKey>
	{
		private readonly IAccountRepository accountRepository;
		private readonly IHistoryService historyService;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<TUser> userManager;
		private readonly RoleManager<TRole> roleManager;
		private readonly IMapper mapper;

		private Func<Data.Account, Core.Account> mapDataToDomain;

		public AccountService(
			IAccountRepository accountRepository,
			IHistoryService historyService,
			IHttpContextAccessor httpContextAccessor,
			UserManager<TUser> userManager,
			RoleManager<TRole> roleManager,
			IMapper mapper)
		{
			this.accountRepository = accountRepository;
			this.historyService = historyService;
			this.httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.mapper = mapper;

			this.mapDataToDomain = this.mapper.Map<Data.Account, Core.Account>;
		}

		public async Task<TKey> GetAuthenticatedAccountIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var user = await this.userManager.GetUserAsync(currentUserClaims);
			if (user == null)
			{
				throw new NullReferenceException(nameof(user));
			}

			return user.Id;
		}

		public Task<string> GetAuthenticatedAccountRoleNameAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var roleName = currentUserClaims.GetAuthenticatedAccountRoleName();
			return Task.FromResult(roleName);
		}

		public async Task<Core.Account> GetAccountAsync(TKey userId, TKey accesibleAccountId)
		{
			var dataAccount = await this.accountRepository.FindByIdsAsync(
				userId.ToString(), 
				accesibleAccountId.ToString());
			var domainAccount = mapDataToDomain(dataAccount);
			return domainAccount;
		}

		public Task<TKey> GetActivatedAccountIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var accountId = currentUserClaims.GetActiveAccountId();
			var genericId = ConvertType(accountId);
			return Task.FromResult(genericId);
		}

		public Task<TKey> GetActivatedAccountRoleIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var roleId = currentUserClaims.GetActiveAccountRoleId();
			var genericId = ConvertType(roleId);
			return Task.FromResult(genericId);
		}

		public async Task<List<Core.Account>> GetAccessibleAccountsAsync()
		{
			var currentUserId = await this.GetActivatedAccountIdAsync();
			var accounts = await this.accountRepository.FindByUserIdAsync(currentUserId.ToString());
			return accounts.Select(mapDataToDomain).ToList();
		}

		public async Task<List<Core.Account>> GetAccessibleAccountsByUserIdAsync(TKey userId)
		{
			var accounts = await this.accountRepository.FindByUserIdAsync(userId.ToString());
			return accounts.Select(mapDataToDomain).ToList();
		}

		public async Task ActivateAccountByIdAsync(TKey accountId)
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			if (currentUserClaims == null)
			{
				throw new NullReferenceException("User claims not found");
			}

			var owner = await this.userManager.GetUserAsync(currentUserClaims);
			if (owner == null)
			{
				throw new NullReferenceException("User for current claims not found");
			}

			var account = await this.GetAccountAsync(owner.Id, accountId);
			if (account == null)
			{
				throw new NullReferenceException("User does not have access to this account");
			}

			var accessibleUser = await this.userManager.FindByIdAsync(accountId.ToString());
			if (accessibleUser == null)
			{
				throw new NullReferenceException("Account not found");
			}

			var accountRoles = await this.userManager.GetRolesAsync(accessibleUser);
			if (accountRoles == null || !accountRoles.Any())
			{
				throw new NullReferenceException("Target user does not have any roles");
			}

			var claimsIdentity = currentUserClaims.Identities.First();
			var accountClaim = claimsIdentity.FindFirst(CoreClaimTypes.ActiveAccountId);
			claimsIdentity.RemoveClaim(accountClaim);

			var accountRoleClaim = claimsIdentity.FindFirst(CoreClaimTypes.ActiveAccountRoleId);
			claimsIdentity.RemoveClaim(accountRoleClaim);

			var loginDateTime = DateTime.UtcNow;
			await this.historyService.UpdateLastLogoutTimeAsync(account, loginDateTime);

			var newAccountClaim = new Claim(CoreClaimTypes.ActiveAccountId, accountId.ToString());
			claimsIdentity.AddClaim(newAccountClaim);

			await this.historyService.AddAsync(account, owner.UserName, accessibleUser.UserName, loginDateTime);

			var role = await this.roleManager.FindByNameAsync(accountRoles.First());
			if (role == null)
			{
				throw new NullReferenceException("Role not found");
			}

			var newAccountRoleClaim = new Claim(CoreClaimTypes.ActiveAccountRoleId, role.Id.ToString());
			claimsIdentity.AddClaim(newAccountRoleClaim);
		}

		public async Task<Core.Account> AddAsync(TKey accountId)
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			if (currentUserClaims == null)
			{
				throw new NullReferenceException("User claims not found");
			}

			var owner = await this.userManager.GetUserAsync(currentUserClaims);
			if (owner == null)
			{
				throw new NullReferenceException("User for current claims not found");
			}

			var existingAccount = await this.GetAccountAsync(owner.Id, accountId);
			if (existingAccount != null)
			{
				throw new ArgumentNullException("User already has access to this account");
			}

			var dataAccount = new Data.Account
			{
				UserId = owner.Id.ToString(),
				AccessibleAccountId = accountId.ToString(),
			};
			var createdAccount = await this.accountRepository.AddAsync(dataAccount);

			var domainAccount = mapDataToDomain(createdAccount);
			return domainAccount;
		}

		public async Task DeleteAsync(TKey accountId)
		{
			var currentUserId = await this.GetAuthenticatedAccountIdAsync();
			var account = await this.accountRepository.FindByIdsAsync(currentUserId.ToString(), accountId.ToString());
			if (account == null)
			{
				return;
			}

			await this.accountRepository.RemoveAsync(account);
		}

		private ClaimsPrincipal GetClaimsFromHttpContext()
		{
			var currentUserClaims = httpContextAccessor.HttpContext?.User;
			if (currentUserClaims == null)
			{
				throw new NullReferenceException(nameof(currentUserClaims));
			}
			return currentUserClaims;
		}

		private static TKey ConvertType(string value)
		{
			return TypeConverter.ConvertType<TKey, string>(value);
		}
	}
}