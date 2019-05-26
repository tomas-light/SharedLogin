namespace Infrastructure.Repositories.Accounts
{
    using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Infrastructure.DbContexts;
	using Infrastructure.Entities;

    public abstract class BaseAccountRepository : IAccountRepository
	{
		private readonly BaseDbContext dbContext;

		public BaseAccountRepository(BaseDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		// get list

		public virtual Task<List<Account>> FindAllAsync()
		{
			return dbContext.Accounts.AsNoTracking().ToListAsync();
		}

		public virtual Task<List<Account>> FindByUserIdAsync(string ownerId)
		{
			return dbContext.Accounts.AsNoTracking()
					.Where(account => account.UserId.Equals(ownerId))
					.ToListAsync();
		}

		public virtual Task<List<Account>> FindByAccessibleAccountIdAsync(string accessibleAccountId)
		{
			return dbContext.Accounts.AsNoTracking()
					.Where(account => account.AccessibleAccountId.Equals(accessibleAccountId))
					.ToListAsync();
		}

		// get one

		public virtual Task<Account> FindByIdsAsync(string ownerId, string accessibleAccountId)
		{
			return dbContext.Accounts.AsNoTracking()
					.FirstOrDefaultAsync(account =>
						account.UserId.Equals(ownerId) &&
						account.AccessibleAccountId.Equals(accessibleAccountId)
					);
		}

		public virtual Task<Account> FindByIdAsync(int id)
		{
			return dbContext.Accounts.AsNoTracking()
					.FirstOrDefaultAsync(account => account.Id == id);
		}

		// create

		public virtual async Task<Account> AddAsync(Account account)
		{
			await dbContext.Accounts.AddAsync(account);
			await dbContext.SaveChangesAsync();
			return account;
		}

		// delete

		public virtual async Task RemoveByIdAsync(int id)
		{
			var account = await this.FindByIdAsync(id);
			dbContext.Accounts.Remove(account);
			await dbContext.SaveChangesAsync();
		}

		public virtual async Task RemoveAsync(Account account)
		{
			dbContext.Accounts.Remove(account);
			await dbContext.SaveChangesAsync();
		}
	}
}
