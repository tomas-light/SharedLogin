namespace Core.Services
{
    using Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountService
	{
		/// <summary>
		/// Get id of current user
		/// </summary>
		/// <returns>user identificator</returns>
		Task<string> GetUserIdAsync();

		/// <summary>
		/// Get account by user id and accesible account id
		/// </summary>
		/// <returns>account</returns>
		Task<Account> GetAccountAsync(string userId, string accesibleAccountId);

		/// <summary>
		/// Get id of activated account for current user
		/// </summary>
		/// <returns>account identificator</returns>
		Task<string> GetActivatedAccountIdAsync();

		/// <summary>
		/// Get list of accessible accounts for current user
		/// </summary>
		/// <returns>accounts list</returns>
		Task<List<Account>> GetAccessibleAccountsAsync();

		/// <summary>
		/// Get list of accessible accounts for specified user by his id
		/// </summary>
		/// <returns>accounts list</returns>
		Task<List<Account>> GetAccessibleAccountsByUserIdAsync(string userId);

		/// <summary>
		/// Set account id to claims if its account accessible for current user
		/// </summary>
		/// <returns></returns>
		Task ActivateAccountIdAsync(string accountId);

		/// <summary>
		/// Allow access current user to account by account id
		/// </summary>
		/// <returns></returns>
		Task<Account> AddAsync(string accountId);

		/// <summary>
		/// Deny access current user to account by account id
		/// </summary>
		/// <returns></returns>
		Task DeleteAsync(string accountId);
	}
}
