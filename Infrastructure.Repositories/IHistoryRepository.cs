namespace Infrastructure.Repositories
{
	using Infrastructure.Entities;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IHistoryRepository
	{
		/// <summary>
		/// Get <see cref="History"/> by id
		/// </summary>
		/// <param name="id"><see cref="History"/> identificator</param>
		/// <returns></returns>
		Task<History> FindByIdAcync(int id);

		/// <summary>
		/// Get list of <see cref="History"/>s by <see cref="Account"/> id
		/// </summary>
		/// <param name="accountId"><see cref="Account"/> identificator</param>
		/// <returns>list of <see cref="History"/>s</returns>
		Task<List<History>> FindByAccountIdAsync(int accountId);

		/// <summary>
		/// Add new <see cref="History"/> and save db context
		/// </summary>
		/// <param name="history"><see cref="History"/></param>
		/// <returns>created <see cref="History"/></returns>
		Task<History> AddAsync(History history);

		/// <summary>
		/// Add existing <see cref="History"/> and save db context
		/// </summary>
		/// <param name="history"><see cref="History"/></param>
		/// <returns>upadted <see cref="History"/></returns>
		Task<History> UpdateAsync(History history);
	}
}
