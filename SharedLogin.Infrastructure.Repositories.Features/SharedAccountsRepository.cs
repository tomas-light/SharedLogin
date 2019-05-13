namespace SharedLogin.Infrastructure.Repositories.Sql.Master
{
    using Microsoft.EntityFrameworkCore;
    using SharedLogin.Domain;
    using SharedLogin.Infrastructure.Contexts;
    using System;
    using System.Collections.Generic;
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

		public Task<IList<SharedAccount>> FindByUserIdAsync(string userId)
		{
			throw new NotImplementedException();
		}

		public Task<IList<SharedAccount>> FindByAccountIdAsync(string accountId)
		{
			throw new NotImplementedException();
		}

		// get one

		public Task<SharedAccount> FindByIdsAsync(string userId, string accountId)
		{
			throw new NotImplementedException();
		}

		public Task<SharedAccount> FindByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		// create

		public Task<SharedAccount> AddAsync(SharedAccount sharedAccount)
		{
			throw new NotImplementedException();
		}

		// delete

		public Task RemoveByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task RemoveAsync(SharedAccount sharedAccount)
		{
			throw new NotImplementedException();
		}
	}
}
