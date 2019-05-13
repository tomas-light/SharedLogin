namespace SharedLogin.Infrastructure.Repositories
{
	using SharedLogin.Domain;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface ISharedAccountsRepository
	{
		// get list

		Task<List<SharedAccount>> FindAllAsync();

		Task<IList<SharedAccount>> FindByUserIdAsync(string userId);

		Task<IList<SharedAccount>> FindByAccountIdAsync(string accountId);

		// get one

		Task<SharedAccount> FindByIdsAsync(string userId, string accountId);

		Task<SharedAccount> FindByIdAsync(int id);

		// create

		Task<SharedAccount> AddAsync(SharedAccount sharedAccount);

		// delete

		Task RemoveByIdAsync(string id);

		Task RemoveAsync(SharedAccount sharedAccount);
	}
}
