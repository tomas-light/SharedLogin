namespace Core.Services
{
    using Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountService<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		/// <summary>
		/// Get id of current user
		/// </summary>
		/// <returns>user identificator</returns>
		Task<TUserPrimaryKey> GetUserIdAsync();

		/// <summary>
		/// Get account by user id and accesible account id
		/// </summary>
		/// <returns>account</returns>
		Task<Account<TUserPrimaryKey>> GetAccountAsync(TUserPrimaryKey userId, TUserPrimaryKey accesibleAccountId);

		/// <summary>
		/// Get id of activated account for current user
		/// </summary>
		/// <returns>account identificator</returns>
		Task<TUserPrimaryKey> GetActivatedAccountIdAsync();

		/// <summary>
		/// Get list of accessible accounts for current user
		/// </summary>
		/// <returns>accounts list</returns>
		Task<List<Account<TUserPrimaryKey>>> GetAccessibleAccountsAsync();

		/// <summary>
		/// Get list of accessible accounts for specified user by his id
		/// </summary>
		/// <returns>accounts list</returns>
		Task<List<Account<TUserPrimaryKey>>> GetAccessibleAccountsByUserIdAsync(TUserPrimaryKey userId);

		/// <summary>
		/// Set account id to claims if its account accessible for current user
		/// </summary>
		/// <returns></returns>
		Task ActivateAccountIdAsync(TUserPrimaryKey accountId);

		/// <summary>
		/// Allow access current user to account by account id
		/// </summary>
		/// <returns></returns>
		Task<Account<TUserPrimaryKey>> AddAsync(TUserPrimaryKey accountId);

		/// <summary>
		/// Deny access current user to account by account id
		/// </summary>
		/// <returns></returns>
		Task DeleteAsync(TUserPrimaryKey accountId);
	}
}
