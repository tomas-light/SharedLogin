namespace SharedLogin.Infrastructure.Repositories.Sql.Master
{
    using Microsoft.EntityFrameworkCore;
    using SharedLogin.Core.DataModels;
    using SharedLogin.Infrastructure.Contexts;
	using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccessHistoryRepository : IAccessHistoryRepository
	{
		private readonly SqlDbContext dbContext;

		public AccessHistoryRepository(SqlDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Task<AccessHistory> FindHistoryByIdAcync(int id)
		{
			return dbContext.AccessHistories.AsNoTracking()
					.FirstOrDefaultAsync(accessHistory => accessHistory.Id == id);
		}

		public Task<List<AccessHistory>> FindHistoriesBySharedAccountIdAsync(int sharedAccountId)
		{
			return dbContext.AccessHistories.AsNoTracking()
					.Where(accessHistory => accessHistory.SharedAccountId == sharedAccountId)
					.ToListAsync();
		}

		public async Task<AccessHistory> AddAsync(AccessHistory accessHistory)
		{
			await dbContext.AccessHistories.AddAsync(accessHistory);
			await dbContext.SaveChangesAsync();
			return accessHistory;
		}

		public async Task<AccessHistory> UpdateAsync(AccessHistory accessHistory)
		{
			dbContext.AccessHistories.Update(accessHistory);
			await dbContext.SaveChangesAsync();
			return accessHistory;
		}
	}
}
