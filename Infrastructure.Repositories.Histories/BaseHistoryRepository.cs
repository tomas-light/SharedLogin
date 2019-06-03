namespace Infrastructure.Repositories.Histories
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

	using Infrastructure.Entities;
    using Infrastructure.DbContexts;

    public class BaseHistoryRepository : IHistoryRepository
	{
		private readonly BaseDbContext dbContext;

		public BaseHistoryRepository(BaseDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Task<History> FindByIdAcync(int id)
		{
			return dbContext.Histories.AsNoTracking()
					.FirstOrDefaultAsync(history => history.Id == id);
		}

		public Task<List<History>> FindByAccountIdAsync(int accountId)
		{
			return dbContext.Histories.AsNoTracking()
					.Where(history => history.AccountId == accountId)
					.ToListAsync();
		}

		public Task<List<History>> FindByAccountIdsAsync(int[] accountIds)
		{
			return dbContext.Histories.AsNoTracking()
					.Where(history => accountIds.Contains(history.AccountId))
					.ToListAsync();
		}

		public async Task<History> AddAsync(History history)
		{
			await dbContext.Histories.AddAsync(history);
			await dbContext.SaveChangesAsync();
			return history;
		}

		public async Task<History> UpdateAsync(History history)
		{
			dbContext.Histories.Update(history);
			await dbContext.SaveChangesAsync();
			return history;
		}
	}
}
