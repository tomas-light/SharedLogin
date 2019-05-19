namespace Infrastructure.Repositories
{
    using Infrastructure.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountRepository
	{
		// get list

		Task<List<Account>> FindAllAsync();

		Task<List<Account>> FindByOwnerIdAsync(string ownerId);

		Task<List<Account>> FindByAccessibleAccountIdAsync(string accessibleAccountId);

		// get one

		Task<Account> FindByIdsAsync(string ownerId, string accessibleAccountId);

		Task<Account> FindByIdAsync(int id);

		// create

		Task<Account> AddAsync(Account account);

		// delete

		Task RemoveByIdAsync(int id);

		Task RemoveAsync(Account account);
	}
}
