namespace SharedLogin.Services.Core
{
	using System;
	using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using SharedLogin.Core.Claims;
	using SharedLogin.Core.DataModels;
	using SharedLogin.Infrastructure.Repositories;

	public class SharedAccountsService : ISharedAccountsService
	{
		private readonly ISharedAccountsRepository sharedAccountsRepository;
		private readonly IAccessHistoryService accessHistoryService;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<IdentityUser> userManager;

		public SharedAccountsService(
			ISharedAccountsRepository sharedAccountsRepository,
			IHttpContextAccessor httpContextAccessor,
			UserManager<IdentityUser> userManager,
			IAccessHistoryService accessHistoryService)
		{
			this.sharedAccountsRepository = sharedAccountsRepository ?? throw new ArgumentNullException(nameof(sharedAccountsRepository));
			this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			this.accessHistoryService = accessHistoryService ?? throw new ArgumentNullException(nameof(accessHistoryService));
		}

		public async Task<string> GetCurrentUserIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var user = await this.userManager.GetUserAsync(currentUserClaims);
			return user.Id;
		}

		public Task<string> GetCurrentAccountIdAsync()
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			var accountId = currentUserClaims.GetAccountId();
			return Task.FromResult(accountId);
		}

		public async Task<IList<SharedAccount>> GetSharedAccountsByCurrentUserAsync()
		{
			var currentUserId = await this.GetCurrentAccountIdAsync();
			var sharedAccounts = await this.sharedAccountsRepository.FindByAccountIdAsync(currentUserId);
			return sharedAccounts;
		}

		public Task<List<SharedAccount>> GetSharedAccountsByUserIdAsync(string userId)
		{
			return this.sharedAccountsRepository.FindByAccountIdAsync(userId);
		}

		public async Task SetCurrentAccountIdAsync(string accountId)
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			if(currentUserClaims == null)
			{
				throw new NullReferenceException("User claims not found");
			}

			var user = await this.userManager.GetUserAsync(currentUserClaims);
			if (user == null)
			{
				throw new NullReferenceException("User for current claims not found");
			}

			var sharedAccount = await this.sharedAccountsRepository.FindByIdsAsync(user.Id, accountId);
			if (sharedAccount == null)
			{
				throw new NullReferenceException("User does not have access to this account");
			}

			var account = await this.userManager.FindByIdAsync(accountId);
			if (account == null)
			{
				throw new NullReferenceException("Account not found");
			}

			var claimsIdentity = currentUserClaims.Identities.First();
			var accountClaim = claimsIdentity.FindFirst(LoginClaimTypes.AccountId);
			claimsIdentity.RemoveClaim(accountClaim);

			var newAccountClaim = new Claim(LoginClaimTypes.AccountId, accountId);
			claimsIdentity.AddClaim(newAccountClaim);

			var dateTime = DateTime.UtcNow;
			await this.accessHistoryService.UpdateLastLogoutTimeForSharedAccountAsync(sharedAccount.Id, dateTime);
			
			var newAccessHistory = new AccessHistory
			{
				SharedAccountId = sharedAccount.Id,
				LoginDateTime = dateTime,
				UserName = user.UserName,
				SharedAccount = sharedAccount,
				AccountName = account.UserName,
			};
			await this.accessHistoryService.AddAsync(newAccessHistory);
		}

		public async Task<SharedAccount> AddAsync(string accountId)
		{
			var currentUserClaims = GetClaimsFromHttpContext();
			if (currentUserClaims == null)
			{
				throw new NullReferenceException("User claims not found");
			}

			var user = await this.userManager.GetUserAsync(currentUserClaims);
			if (user == null)
			{
				throw new NullReferenceException("User for current claims not found");
			}

			var existingSharedAccount = await this.sharedAccountsRepository.FindByIdsAsync(user.Id, accountId);
			if (existingSharedAccount != null)
			{
				throw new ArgumentNullException("User already has access to this account");
			}

			var sharedAccount = new SharedAccount
			{
				UserId = user.Id,
				AccountId = accountId,
			};

			await this.sharedAccountsRepository.AddAsync(sharedAccount);
			return sharedAccount;
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
