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
	using Data = Infrastructure.Models;
	using Domain = Core.Models;
	using Infrastructure.Repositories;
	using Utils;

    public class AccountService : IAccountService
	{
		private readonly IAccountRepository accountRepository;
		private readonly IHistoryService historyService;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IMapper mapper;

		private Func<Data.Account, Domain.Account> mapDataToDomain;
		private Func<Domain.Account, Data.Account> mapDomainToData;

		public AccountService(
			IAccountRepository accountRepository,
			IHistoryService historyService,
			IHttpContextAccessor httpContextAccessor,
			UserManager<IdentityUser> userManager,
			IMapper mapper)
		{
			this.accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
			this.historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
			this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
			var dataAccount = await this.accountRepository.FindByIdsAsync(userId, accesibleAccountId);
			if (dataAccount == null)
			{
				throw new NullReferenceException("User does not have access to this account");
			}

			var domainAccount = mapDataToDomain(dataAccount);
			return domainAccount;
		}

		public Task<string> GetActivatedAccountIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var accountId = currentUserClaims.GetActiveAccountId();
			var genericId = ConvertType(accountId);
			return Task.FromResult(genericId);
		}

		public async Task<List<Domain.Account>> GetAccessibleAccountsAsync()
		{
			var currentUserId = await this.GetActivatedAccountIdAsync();
			var accounts = await this.accountRepository.FindByOwnerIdAsync(currentUserId);
			return accounts.Select(mapDataToDomain).ToList();
		}

		public async Task<List<Domain.Account>> GetAccessibleAccountsByUserIdAsync(string userId)
		{
			var accounts = await this.accountRepository.FindByOwnerIdAsync(userId);
			return accounts.Select(mapDataToDomain).ToList();
		}

		public async Task ActivateAccountIdAsync(string accountId)
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

			var claimsIdentity = currentUserClaims.Identities.First();
			var accountClaim = claimsIdentity.FindFirst(CoreClaimTypes.ActiveAccountId);
			claimsIdentity.RemoveClaim(accountClaim);

			var newAccountClaim = new Claim(CoreClaimTypes.ActiveAccountId, accountId.ToString());
			claimsIdentity.AddClaim(newAccountClaim);

			await this.historyService.UpdateLastLogoutTimeAsync(account, owner, accessibleUser);
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

			var account = new Domain.Account
			{
				OwnerId = owner.Id,
				AccessibleAccountId = accountId,
			};
			var dataAccount = mapDomainToData(account);
			var createdAccount = await this.accountRepository.AddAsync(dataAccount);

			account = mapDataToDomain(createdAccount);
			return account;
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
		
		private static string ConvertType(string value)
		{
			return TypeConverter.ConvertType<string, string>(value);
		}
	}
}