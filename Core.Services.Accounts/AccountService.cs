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
	using Data = Infrastructure.Entities;
	using Domain = Core.Models;
	using Infrastructure.Repositories;

	public class AccountService : IAccountService
	{
		private readonly IAccountRepository accountRepository;
		private readonly IHistoryService historyService;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<IdentityUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IMapper mapper;

		private Func<Data.Account, Domain.Account> mapDataToDomain;
		private Func<Domain.Account, Data.Account> mapDomainToData;

		public AccountService(
			IAccountRepository accountRepository,
			IHistoryService historyService,
			IHttpContextAccessor httpContextAccessor,
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IMapper mapper)
		{
			this.accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
			this.historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
			this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

			this.mapDataToDomain = this.mapper.Map<Data.Account, Domain.Account>;
			this.mapDomainToData = this.mapper.Map<Domain.Account, Data.Account>;
		}

		public async Task<string> GetUserIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var user = await this.userManager.GetUserAsync(currentUserClaims);
			if (user == null)
			{
				throw new NullReferenceException(nameof(user));
			}

			return user.Id;
		}

		public async Task<Domain.Account> GetAccountAsync(string userId, string accesibleAccountId)
		{
			var dataAccount = await this.accountRepository.FindByIdsAsync(
				userId, 
				accesibleAccountId);
			var domainAccount = mapDataToDomain(dataAccount);
			return domainAccount;
		}

		public Task<string> GetActivatedAccountIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var accountId = currentUserClaims.GetActiveAccountId();
			return Task.FromResult(accountId);
		}

		public Task<string> GetActivatedAccountRoleIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var roleId = currentUserClaims.GetActiveAccountRoleId();
			return Task.FromResult(roleId);
		}

		public async Task<List<Domain.Account>> GetAccessibleAccountsAsync()
		{
			var currentUserId = await this.GetActivatedAccountIdAsync();
			var accounts = await this.accountRepository.FindByUserIdAsync(currentUserId);
			return accounts.Select(mapDataToDomain).ToList();
		}

		public async Task<List<Domain.Account>> GetAccessibleAccountsByUserIdAsync(string userId)
		{
			var accounts = await this.accountRepository.FindByUserIdAsync(userId);
			return accounts.Select(mapDataToDomain).ToList();
		}

		public async Task ActivateAccountByIdAsync(string accountId)
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

			var newAccountRoleClaim = new Claim(CoreClaimTypes.ActiveAccountRoleId, role.Id);
			claimsIdentity.AddClaim(newAccountRoleClaim);
		}

		public async Task<Domain.Account> AddAsync(string accountId)
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
				UserId = owner.Id,
				AccessibleAccountId = accountId,
			};
			var createdAccount = await this.accountRepository.AddAsync(dataAccount);

			var domainAccount = mapDataToDomain(createdAccount);
			return domainAccount;
		}

		public async Task DeleteAsync(string accountId)
		{
			var currentUserId = await this.GetUserIdAsync();
			var account = await this.accountRepository.FindByIdsAsync(currentUserId, accountId);
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
	}
}