namespace SharedLogin.Services
{
    using SharedLogin.Core.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccessHistoryService : IService
	{
		Task<AccessHistory> GetHistoryByIdAcync(int id);

		Task<List<AccessHistory>> GetHistoriesBySharedAccountIdAsync(int sharedAccountId);

		Task<AccessHistory> GetLastHistoryBySharedAccountIdAsync(int sharedAccountId);

		Task<AccessHistory> AddAsync(AccessHistory accessHistory);

		Task<AccessHistory> UpdateAsync(AccessHistory accessHistory);

		Task UpdateLastLogoutTimeForSharedAccountAsync(int sharedAccountId, DateTime dateTime);
	}
}
