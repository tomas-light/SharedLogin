namespace Infrastructure.Repositories.Accounts
{
    using Infrastructure.DbContexts;
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountSqlRepository<TUserPrimaryKey> : IAccountRepository<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		private readonly SqlDbContext<TUserPrimaryKey> dbContext;

		public AccountSqlRepository(SqlDbContext<TUserPrimaryKey> dbContext)
		{
			this.dbContext = dbContext;
		}

		// get list

		public Task<List<Account<TUserPrimaryKey>>> FindAllAsync()
		{
			return dbContext.Accounts.AsNoTracking().ToListAsync();
		}

		public Task<List<Account<TUserPrimaryKey>>> FindByOwnerIdAsync(TUserPrimaryKey ownerId)
		{
			return dbContext.Accounts.AsNoTracking()
					.Where(account => account.OwnerId.Equals(ownerId))
					.ToListAsync();
		}

		public Task<List<Account<TUserPrimaryKey>>> FindByAccessibleAccountIdAsync(TUserPrimaryKey accessibleAccountId)
		{
			return dbContext.Accounts.AsNoTracking()
					.Where(account => account.AccessibleAccountId.Equals(accessibleAccountId))
					.ToListAsync();
		}

		// get one

		public Task<Account<TUserPrimaryKey>> FindByIdsAsync(TUserPrimaryKey ownerId, TUserPrimaryKey accessibleAccountId)
		{
			return dbContext.Accounts.AsNoTracking()
					.FirstOrDefaultAsync(account =>
						account.OwnerId.Equals(ownerId) &&
						account.AccessibleAccountId.Equals(accessibleAccountId)
					);
		}

		public Task<Account<TUserPrimaryKey>> FindByIdAsync(int id)
		{
			return dbContext.Accounts.AsNoTracking()
					.FirstOrDefaultAsync(account => account.Id == id);
		}

		// create

		public async Task<Account<TUserPrimaryKey>> AddAsync(Account<TUserPrimaryKey> account)
		{
			await dbContext.Accounts.AddAsync(account);
			await dbContext.SaveChangesAsync();
			return account;
		}

		// delete

		public async Task RemoveByIdAsync(int id)
		{
			var account = await this.FindByIdAsync(id);
			dbContext.Accounts.Remove(account);
			await dbContext.SaveChangesAsync();
		}

		public async Task RemoveAsync(Account<TUserPrimaryKey> account)
		{
			dbContext.Accounts.Remove(account);
			await dbContext.SaveChangesAsync();
		}
	}
}
