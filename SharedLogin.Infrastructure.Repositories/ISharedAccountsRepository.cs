namespace SharedLogin.Infrastructure.Repositories
{
	using SharedLogin.Core.DataModels;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface ISharedAccountsRepository : IRepository
	{
		// get list

		Task<List<SharedAccount>> FindAllAsync();

		Task<List<SharedAccount>> FindByUserIdAsync(string userId);

		Task<List<SharedAccount>> FindByAccountIdAsync(string accountId);

		// get one

		Task<SharedAccount> FindByIdsAsync(string userId, string accountId);

		Task<SharedAccount> FindByIdAsync(int id);

		// create

		Task<SharedAccount> AddAsync(SharedAccount sharedAccount);

		// delete

		Task RemoveByIdAsync(int id);

		Task RemoveAsync(SharedAccount sharedAccount);
	}
}
