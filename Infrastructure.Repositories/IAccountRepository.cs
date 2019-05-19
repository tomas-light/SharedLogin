namespace Infrastructure.Repositories
{
    using Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountRepository<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		// get list

		Task<List<Account<TUserPrimaryKey>>> FindAllAsync();

		Task<List<Account<TUserPrimaryKey>>> FindByOwnerIdAsync(TUserPrimaryKey ownerId);

		Task<List<Account<TUserPrimaryKey>>> FindByAccessibleAccountIdAsync(TUserPrimaryKey accessibleAccountId);

		// get one

		Task<Account<TUserPrimaryKey>> FindByIdsAsync(TUserPrimaryKey ownerId, TUserPrimaryKey accessibleAccountId);

		Task<Account<TUserPrimaryKey>> FindByIdAsync(int id);

		// create

		Task<Account<TUserPrimaryKey>> AddAsync(Account<TUserPrimaryKey> account);

		// delete

		Task RemoveByIdAsync(int id);

		Task RemoveAsync(Account<TUserPrimaryKey> account);
	}
}
