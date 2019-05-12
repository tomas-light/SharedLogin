namespace SharedLogin.Infrastructure.Repositories
{
	using SharedLogin.Domain;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface ISharedAccountsRepository<TAccountPrimaryKey>
	{
		// get list

		Task<IList<SharedAccount<TAccountPrimaryKey>>> FindAllAsync();

		Task<IList<SharedAccount<TAccountPrimaryKey>>> FindByUserIdAsync(TAccountPrimaryKey userId);

		Task<IList<SharedAccount<TAccountPrimaryKey>>> FindByAccountIdAsync(TAccountPrimaryKey accountId);

		// get one

		Task<SharedAccount<TAccountPrimaryKey>> FindByIdsAsync(TAccountPrimaryKey userId, TAccountPrimaryKey accountId);

		Task<SharedAccount<TAccountPrimaryKey>> FindByIdAsync(int id);

		// create

		Task<SharedAccount<TAccountPrimaryKey>> AddAsync(SharedAccount<TAccountPrimaryKey> sharedAccount);

		// delete

		Task RemoveByIdAsync(TAccountPrimaryKey id);

		Task RemoveAsync(SharedAccount<TAccountPrimaryKey> sharedAccount);
	}
}
