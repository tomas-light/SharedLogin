namespace SharedLogin.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

	public static class SharedLoginConfiguration
	{
		public static void Configure(IServiceCollection services, IConfiguration configuration, string dbConnectionString)
		{
			IocConfiguration.Configure(services);
			ContextConfiguration.Configure(services, dbConnectionString);
		}
	}
}
