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
					.Where(account => account.UserId.Equals(ownerId) && account.IsAllow)
					.ToListAsync();
		}

		public virtual Task<List<Account>> FindByAccessibleAccountIdAsync(string accessibleAccountId)
		{
			return dbContext.Accounts.AsNoTracking()
					.Where(account => account.AccessibleAccountId.Equals(accessibleAccountId) && account.IsAllow)
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
			var existingAccount = dbContext.Accounts.FirstOrDefault(a => 
														a.Id == account.Id || 
														a.UserId == account.UserId && 
														a.AccessibleAccountId == account.AccessibleAccountId);
			if (existingAccount == null)
			{
				await dbContext.Accounts.AddAsync(account);
			}
			else
			{
				existingAccount.IsAllow = true;
				dbContext.Accounts.Update(account);
			}

			await dbContext.SaveChangesAsync();
			return account;
		}

		// delete

		public virtual async Task RemoveByIdAsync(int id)
		{
			var account = await this.FindByIdAsync(id);
			account.IsAllow = false;
			dbContext.Accounts.Update(account);
			await dbContext.SaveChangesAsync();
		}

		public virtual async Task RemoveAsync(Account account)
		{
			account.IsAllow = false;
			dbContext.Accounts.Update(account);
			await dbContext.SaveChangesAsync();
		}
	}
}
