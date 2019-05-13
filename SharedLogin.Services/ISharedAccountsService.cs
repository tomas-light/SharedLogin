namespace SharedLogin.Services
{
	using SharedLogin.Domain;
	using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISharedAccountsService : IService
	{
		Task<string> GetCurrentUserIdAsync();

		Task<string> GetCurrentAccountIdAsync();

		Task<IList<SharedAccount>> GetCurrentSharedAccountsByCurrentUserAsync();

		Task<IList<SharedAccount>> GetCurrentSharedAccountsByUserIdAsync(string userId);

		Task<SharedAccount> AddAsync();
	}
}
