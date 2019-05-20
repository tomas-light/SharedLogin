namespace Infrastructure.Repositories
{
	using Infrastructure.Entities;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IAccountRepository
	{
		/// <summary>
		/// Get all <see cref="Account"/>s
		/// </summary>
		/// <returns>list of <see cref="Account"/>s</returns>
		Task<List<Account>> FindAllAsync();

		/// <summary>
		/// Get <see cref="Account"/>s that accessible to user by his id
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <returns>list of <see cref="Account"/>s</returns>
		Task<List<Account>> FindByUserIdAsync(string userId);

		/// <summary>
		/// Get <see cref="Account"/>s by accessible <see cref="Account"/> id
		/// </summary>
		/// <param name="accessibleAccountId">accessible <see cref="Account"/> identificator</param>
		/// <returns>list of accounts</returns>
		Task<List<Account>> FindByAccessibleAccountIdAsync(string accessibleAccountId);

		/// <summary>
		/// Get <see cref="Account"/> by user id and accessible account id
		/// </summary>
		/// <param name="userId">user identificator</param>
		/// <param name="accessibleAccountId">accessible <see cref="Account"/> identificator</param>
		/// <returns><see cref="Account"/> or null</returns>
		Task<Account> FindByIdsAsync(string userId, string accessibleAccountId);

		/// <summary>
		/// Get account by id
		/// </summary>
		/// <param name="id"><see cref="Account"/> identificator</param>
		/// <returns><see cref="Account"/> or null</returns>
		Task<Account> FindByIdAsync(int id);

		/// <summary>
		/// Add new <see cref="Account"/> and save db context
		/// </summary>
		/// <param name="account"></param>
		/// <returns>created <see cref="Account"/></returns>
		Task<Account> AddAsync(Account account);

		/// <summary>
		/// Remove existing <see cref="Account"/> and save db context
		/// </summary>
		/// <param name="id"><see cref="Account"/> identificator</param>
		/// <returns></returns>
		Task RemoveByIdAsync(int id);

		/// <summary>
		/// Remove <see cref="Account"/> if it exists and save db context
		/// </summary>
		/// <param name="account">existing <see cref="Account"/></param>
		/// <returns></returns>
		Task RemoveAsync(Account account);
	}
}
