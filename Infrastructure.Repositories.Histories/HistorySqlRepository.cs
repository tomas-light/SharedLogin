namespace Infrastructure.Repositories.Histories
{
    using Infrastructure.DbContexts;
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class HistorySqlRepository<TUserPrimaryKey> : IHistoryRepository<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		private readonly SqlDbContext<TUserPrimaryKey> dbContext;

		public HistorySqlRepository(SqlDbContext<TUserPrimaryKey> dbContext)
		{
			this.dbContext = dbContext;
		}

		public Task<History<TUserPrimaryKey>> FindByIdAcync(int id)
		{
			return dbContext.Histories.AsNoTracking()
					.FirstOrDefaultAsync(history => history.Id == id);
		}

		public Task<List<History<TUserPrimaryKey>>> FindByAccountIdAsync(int accountId)
		{
			return dbContext.Histories.AsNoTracking()
					.Where(history => history.AccountId == accountId)
					.ToListAsync();
		}

		public async Task<History<TUserPrimaryKey>> AddAsync(History<TUserPrimaryKey> history)
		{
			await dbContext.Histories.AddAsync(history);
			await dbContext.SaveChangesAsync();
			return history;
		}

		public async Task<History<TUserPrimaryKey>> UpdateAsync(History<TUserPrimaryKey> history)
		{
			dbContext.Histories.Update(history);
			await dbContext.SaveChangesAsync();
			return history;
		}
	}
}
