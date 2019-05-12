namespace SharedLogin.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
	using SharedLogin.Infrastructure.Repositories;
	using SharedLogin.Infrastructure.Repositories.Sql.Master;

	public class IoC
	{
		private readonly IServiceCollection services;

		public IoC(IServiceCollection services)
		{
			this.services = services;
		}

		public void RegisterRepository<TAccountPrimaryKey>()
		{
			this.services.AddScoped<ISharedAccountsRepository<TAccountPrimaryKey>, SharedAccountsRepository<TAccountPrimaryKey>>();
		}
	}
}
