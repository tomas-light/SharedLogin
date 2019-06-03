namespace Core.Services
{
	using Core.Models;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

	public interface IAccountService<TUser, TRole, TKey>
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TKey : IEquatable<TKey>
	{
		/// <summary>
		/// Get id of current user
		/// </summary>
		/// <returns>user identificator</returns>
		Task<TKey> GetAuthenticatedAccountIdAsync();

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
		Task<Account> GetAccountAsync(TKey userId, TKey accesibleAccountId);

		/// <summary>
		/// Get id of activated <see cref="Account"/>
		/// </summary>
		/// <returns><see cref="Account"/> identificator</returns>
		Task<TKey> GetActivatedAccountIdAsync();

		/// <summary>
		/// Get id role of activated <see cref="Account"/>
		/// </summary>
		/// <returns>role identificator</returns>
		Task<TKey> GetActivatedAccountRoleIdAsync();

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
		Task<List<Account>> GetAccessibleAccountsByUserIdAsync(TKey userId);

		/// <summary>
		/// Get a list of accounts that have access to the current account
		/// </summary>
		/// <param name="userId">identificator of current user</param>
		/// <returns></returns>
		Task<List<Account>> GetAccountsThatHaveAccess(TKey userId);

		/// <summary>
		/// Generate jwt token for current user
		/// </summary>
		/// <returns></returns>
		Task<string> GetCurrentTokenAsync();

		/// <summary>
		/// Set <see cref="Account"/> id to claims if its <see cref="Account"/> accessible for current user
		/// </summary>
		/// <param name="accessibleAccountId"><see cref="Account"/> identificator</param>
		/// <returns></returns>
		Task<string> ActivateAccountByIdAsync(TKey accessibleAccountId);

		/// <summary>
		/// Set <see cref="Account"/> id to claims if its <see cref="Account"/> accessible for current user
		/// </summary>
		/// <returns></returns>
		Task<string> ActivateAccountByIdAsync(ClaimsPrincipal currentUserClaims, TUser owner, TKey accessibleAccountId);

		/// <summary>
		/// Allow access to current user <see cref="Account"/> for specified user by his id
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <returns>allowed account</returns>
		Task<Account> AddAsync(TKey usertId);

		/// <summary>
		/// Allow access to specified user <see cref="Account"/> for specified account by his id and activate it
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <param name="accountId">account identificator</param>
		/// <returns>allowed account</returns>
		Task<Account> AddAndActivateAsync(TKey usertId, TKey accountId);

		/// <summary>
		/// Deny access to current user <see cref="Account"/> for specified user by his id
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <returns></returns>
		Task DeleteAsync(TKey usertId);
	}
}
