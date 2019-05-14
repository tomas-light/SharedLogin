namespace SharedLogin.Infrastructure.Repositories.Sql.Master
{
    using Microsoft.EntityFrameworkCore;
    using SharedLogin.Core.DataModels;
    using SharedLogin.Infrastructure.Contexts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SharedAccountsRepository : ISharedAccountsRepository
	{
		private readonly SqlDbContext dbContext;

		public SharedAccountsRepository(SqlDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		// get list

		public Task<List<SharedAccount>> FindAllAsync()
		{
			return dbContext.SharedAccounts.AsNoTracking().ToListAsync();
		}

		public Task<List<SharedAccount>> FindByUserIdAsync(string userId)
		{
			return dbContext.SharedAccounts.AsNoTracking()
					.Where(sharedAccount => sharedAccount.UserId == userId)
					.ToListAsync();
		}

		public Task<List<SharedAccount>> FindByAccountIdAsync(string accountId)
		{
			return dbContext.SharedAccounts.AsNoTracking()
					.Where(sharedAccount => sharedAccount.AccountId == accountId)
					.ToListAsync();
		}

		// get one

		public Task<SharedAccount> FindByIdsAsync(string userId, string accountId)
		{
			return dbContext.SharedAccounts.AsNoTracking()
					.FirstOrDefaultAsync(sharedAccount => 
						sharedAccount.UserId == userId && 
						sharedAccount.AccountId == accountId
					);
		}

		public Task<SharedAccount> FindByIdAsync(int id)
		{
			return dbContext.SharedAccounts.AsNoTracking()
					.FirstOrDefaultAsync(sharedAccount => sharedAccount.Id == id);
		}

		// create

		public async Task<SharedAccount> AddAsync(SharedAccount sharedAccount)
		{
			await dbContext.SharedAccounts.AddAsync(sharedAccount);
			await dbContext.SaveChangesAsync();
			return sharedAccount;
		}

		// delete

		public async Task RemoveByIdAsync(int id)
		{
			var sharedAccount = await this.FindByIdAsync(id);
			dbContext.SharedAccounts.Remove(sharedAccount);
			await dbContext.SaveChangesAsync();
		}

		public async Task RemoveAsync(SharedAccount sharedAccount)
		{
			dbContext.SharedAccounts.Remove(sharedAccount);
			await dbContext.SaveChangesAsync();
		}
	}
}
