namespace SharedLogin.Infrastructure.Repositories.Sql.Master
{
    using Microsoft.EntityFrameworkCore;
    using SharedLogin.Domain;
    using SharedLogin.Infrastructure.Contexts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SharedAccountsRepository<TAccountPrimaryKey> : ISharedAccountsRepository<TAccountPrimaryKey>
	{
		private readonly SqlDbContext<TAccountPrimaryKey> dbContext;

		public SharedAccountsRepository(SqlDbContext<TAccountPrimaryKey> dbContext)
		{
			this.dbContext = dbContext;
		}

		// get list

		public Task<IList<SharedAccount<TAccountPrimaryKey>>> FindAllAsync()
		{
			var result = dbContext.SharedAccounts.AsNoTracking().Cast<IList<SharedAccount<TAccountPrimaryKey>>>().ToListAsync();
			return result as Task<IList<SharedAccount<TAccountPrimaryKey>>>;
		}

		public Task<IList<SharedAccount<TAccountPrimaryKey>>> FindByUserIdAsync(TAccountPrimaryKey userId)
		{
			throw new NotImplementedException();
		}

		public Task<IList<SharedAccount<TAccountPrimaryKey>>> FindByAccountIdAsync(TAccountPrimaryKey accountId)
		{
			throw new NotImplementedException();
		}

		// get one

		public Task<SharedAccount<TAccountPrimaryKey>> FindByIdsAsync(TAccountPrimaryKey userId, TAccountPrimaryKey accountId)
		{
			throw new NotImplementedException();
		}

		public Task<SharedAccount<TAccountPrimaryKey>> FindByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		// create

		public Task<SharedAccount<TAccountPrimaryKey>> AddAsync(SharedAccount<TAccountPrimaryKey> sharedAccount)
		{
			throw new NotImplementedException();
		}

		// delete

		public Task RemoveByIdAsync(TAccountPrimaryKey id)
		{
			throw new NotImplementedException();
		}

		public Task RemoveAsync(SharedAccount<TAccountPrimaryKey> sharedAccount)
		{
			throw new NotImplementedException();
		}
	}
}
