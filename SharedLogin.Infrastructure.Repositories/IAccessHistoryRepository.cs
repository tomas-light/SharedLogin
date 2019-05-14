namespace SharedLogin.Infrastructure.Repositories
{
    using SharedLogin.Core.DataModels;
	using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccessHistoryRepository : IRepository
	{
		Task<AccessHistory> FindHistoryByIdAcync(int id);

		Task<List<AccessHistory>> FindHistoriesBySharedAccountIdAsync(int sharedAccountId);

		Task<AccessHistory> AddAsync(AccessHistory accessHistory);

		Task<AccessHistory> UpdateAsync(AccessHistory accessHistory);
	}
}
