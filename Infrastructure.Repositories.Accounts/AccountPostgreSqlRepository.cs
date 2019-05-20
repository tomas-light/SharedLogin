namespace Infrastructure.Repositories.Accounts
{
	using Infrastructure.DbContexts;
	using Infrastructure.Entities;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class AccountPostgreSqlRepository : IAccountRepository
	{
		private readonly PostgreSqlDbContext dbContext;

		public AccountPostgreSqlRepository(PostgreSqlDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		// get list

		public Task<List<Account>> FindAllAsync()
		{
			return dbContext.Accounts.AsNoTracking().ToListAsync();
		}

		public Task<List<Account>> FindByUserIdAsync(string ownerId)
		{
			return dbContext.Accounts.AsNoTracking()
					.Where(account => account.UserId.Equals(ownerId))
					.ToListAsync();
		}

		public Task<List<Account>> FindByAccessibleAccountIdAsync(string accessibleAccountId)
		{
			return dbContext.Accounts.AsNoTracking()
					.Where(account => account.AccessibleAccountId.Equals(accessibleAccountId))
					.ToListAsync();
		}

		// get one

		public Task<Account> FindByIdsAsync(string ownerId, string accessibleAccountId)
		{
			return dbContext.Accounts.AsNoTracking()
					.FirstOrDefaultAsync(account =>
						account.UserId.Equals(ownerId) &&
						account.AccessibleAccountId.Equals(accessibleAccountId)
					);
		}

		public Task<Account> FindByIdAsync(int id)
		{
			return dbContext.Accounts.AsNoTracking()
					.FirstOrDefaultAsync(account => account.Id == id);
		}

		// create

		public async Task<Account> AddAsync(Account account)
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

		public async Task RemoveAsync(Account account)
		{
			dbContext.Accounts.Remove(account);
			await dbContext.SaveChangesAsync();
		}
	}
}
