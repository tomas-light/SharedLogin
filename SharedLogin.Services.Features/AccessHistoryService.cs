namespace SharedLogin.Services.Core
{
    using SharedLogin.Core.DataModels;
    using SharedLogin.Infrastructure.Repositories;
    using System;
	using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccessHistoryService : IAccessHistoryService
	{
		private readonly IAccessHistoryRepository repository;

		public AccessHistoryService(IAccessHistoryRepository accessHistoryRepository)
		{
			this.repository = accessHistoryRepository ?? throw new ArgumentNullException(nameof(accessHistoryRepository));
		}

		public Task<AccessHistory> GetHistoryByIdAcync(int id)
		{
			return this.repository.FindHistoryByIdAcync(id);
		}

		public Task<List<AccessHistory>> GetHistoriesBySharedAccountIdAsync(int sharedAccountId)
		{
			return this.repository.FindHistoriesBySharedAccountIdAsync(sharedAccountId);
		}

		public async Task<AccessHistory> GetLastHistoryBySharedAccountIdAsync(int sharedAccountId)
		{
			var histories = await this.repository.FindHistoriesBySharedAccountIdAsync(sharedAccountId);
			if (!histories.Any())
			{
				return null;
			}

			var lastHistory = histories.OrderByDescending(history => history.LoginDateTime).First();
			return lastHistory;
		}

		public Task<AccessHistory> AddAsync(AccessHistory accessHistory)
		{
			return this.repository.AddAsync(accessHistory);
		}

		public Task<AccessHistory> UpdateAsync(AccessHistory accessHistory)
		{
			return this.repository.UpdateAsync(accessHistory);
		}

		public async Task UpdateLastLogoutTimeForSharedAccountAsync(int sharedAccountId, DateTime dateTime)
		{
			var lastAccessHistory = await this.GetLastHistoryBySharedAccountIdAsync(sharedAccountId);
			if (lastAccessHistory != null)
			{
				lastAccessHistory.EndLoginDateTime = dateTime;
				await this.UpdateAsync(lastAccessHistory);
			}
		}
	}
}
