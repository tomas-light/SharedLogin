namespace Core.Services
{
	using Core.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IAccountService
	{
		/// <summary>
		/// Get id of current user
		/// </summary>
		/// <returns>user identificator</returns>
		Task<string> GetAuthenticatedAccountIdAsync();

		/// <summary>
		/// Get name role of authenticated user
		/// </summary>
		/// <returns>role name</returns>
		Task<string> GetAuthenticatedAccountRoleNameAsync();

		/// <summary>
		/// Get <see cref="Account"/> by user id and accesible <see cref="Account"/> id
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <param name="accesibleAccountId">accessible <see cref="Account"/> identificator</param>
		/// <returns><see cref="Account"/></returns>
		Task<Account> GetAccountAsync(string userId, string accesibleAccountId);

		/// <summary>
		/// Get id of activated <see cref="Account"/>
		/// </summary>
		/// <returns><see cref="Account"/> identificator</returns>
		Task<string> GetActivatedAccountIdAsync();

		/// <summary>
		/// Get id role of activated <see cref="Account"/>
		/// </summary>
		/// <returns>role identificator</returns>
		Task<string> GetActivatedAccountRoleIdAsync();

		/// <summary>
		/// Get list of accessible <see cref="Account"/>s for current user
		/// </summary>
		/// <returns>list of <see cref="Account"/>s</returns>
		Task<List<Account>> GetAccessibleAccountsAsync();

		/// <summary>
		/// Get list of accessible <see cref="Account"/>s for specified user by his id
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <returns>list of <see cref="Account"/>s</returns>
		Task<List<Account>> GetAccessibleAccountsByUserIdAsync(string userId);

		/// <summary>
		/// Set <see cref="Account"/> id to claims if its <see cref="Account"/> accessible for current user
		/// </summary>
		/// <param name="accountId"><see cref="Account"/> identificator</param>
		/// <returns></returns>
		Task ActivateAccountByIdAsync(string accountId);

		/// <summary>
		/// Allow access to current user <see cref="Account"/> for specified user by his id
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <returns>allowed account</returns>
		Task<Account> AddAsync(string usertId);

		/// <summary>
		/// Deny access to current user <see cref="Account"/> for specified user by his id
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <returns></returns>
		Task DeleteAsync(string usertId);
	}
}
