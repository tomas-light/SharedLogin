namespace Core.Services
{
	using Core.Models;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IHistoryService
	{
		/// <summary>
		/// Get <see cref="History"/> by history id
		/// </summary>
		/// <param name="id"><see cref="History"/> identificator</param>
		/// <returns><see cref="History"/></returns>
		Task<History> GetByIdAcync(int id);

		/// <summary>
		/// Get list of <see cref="History"/> by account id
		/// </summary>
		/// <param name="accountId"><see cref="Account"/> identificator</param>
		/// <returns>list of <see cref="History"/>s</returns>
		Task<List<History>> GetByAccountIdAsync(int accountId);

		/// <summary>
		/// Get list of <see cref="History"/> by <see cref="Account"/> id list
		/// </summary>
		/// <param name="accountIds"><see cref="Account"/> identificators</param>
		/// <returns>list of <see cref="History"/>s</returns>
		Task<List<History>> GetByAccountIdsAsync(int[] accountIds);

		/// <summary>
		/// Add information about <see cref="Account"/> authorization to <see cref="History"/>
		/// </summary>
		/// <param name="account">authorized <see cref="Account"/></param>
		/// <param name="userName">name of authorized user</param>
		/// <param name="accessibleAccountNamem">name of authorized <see cref="Account"/></param>
		/// <param name="loginDateTime">authorization time</param>
		/// <returns>added <see cref="History"/></returns>
		Task<History> AddAsync(
			Account account,
			string userName,
			string accessibleAccountNamem,
			DateTime loginDateTime);

		/// <summary>
		/// Update logout time for autorized <see cref="Account"/>
		/// </summary>
		/// <param name="account">authorized <see cref="Account"/></param>
		/// <param name="loginDateTime">authorization end time</param>
		/// <returns></returns>
		Task UpdateLastLogoutTimeAsync(Account account, DateTime logoutDateTime);
	}
}
