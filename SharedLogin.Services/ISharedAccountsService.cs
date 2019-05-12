namespace SharedLogin.Services
{
	using SharedLogin.Domain;
	using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISharedAccountsService<TAccountPrimaryKey> : IService
	{
		Task<TAccountPrimaryKey> GetCurrentUserIdAsync();

		Task<TAccountPrimaryKey> GetCurrentAccountIdAsync();

		Task<IList<SharedAccount<TAccountPrimaryKey>>> GetCurrentSharedAccountsByCurrentUserAsync();

		Task<IList<SharedAccount<TAccountPrimaryKey>>> GetCurrentSharedAccountsByUserIdAsync(TAccountPrimaryKey userId);

		Task<SharedAccount<TAccountPrimaryKey>> AddAsync();
	}
}
