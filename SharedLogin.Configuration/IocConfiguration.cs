namespace SharedLogin.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
	using SharedLogin.Infrastructure.Repositories;
	using SharedLogin.Infrastructure.Repositories.Sql.Master;

	public static class IocConfiguration
	{
		public static void Configure(IServiceCollection services)
		{
			RegisterRepository(services);
		}

		public static void RegisterRepository(IServiceCollection services)
		{
			services.AddScoped<ISharedAccountsRepository, SharedAccountsRepository>();
		}
	}
}
