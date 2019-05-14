namespace SharedLogin.Services
{
	using SharedLogin.Core.DataModels;
	using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISharedAccountsService : IService
	{
		Task<string> GetCurrentUserIdAsync();

		Task<string> GetCurrentAccountIdAsync();

		Task<IList<SharedAccount>> GetSharedAccountsByCurrentUserAsync();

		Task<List<SharedAccount>> GetSharedAccountsByUserIdAsync(string userId);
		
		Task SetCurrentAccountIdAsync(string accountId);

		Task<SharedAccount> AddAsync(string accountId);
	}
}
