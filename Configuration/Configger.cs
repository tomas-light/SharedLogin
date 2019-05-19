namespace Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

	public static class Configger
	{
		public static void Configure(IServiceCollection services, IConfiguration configuration, string dbConnectionString, DbConfigurationOptions dbConfigurationOptions = DbConfigurationOptions.Sql)
		{
			MapperConfiguration.Configure();
			DbContextConfiguration.Configure(services, dbConnectionString, dbConfigurationOptions);
			DependencyConfiguration.Configure(services, dbConfigurationOptions);
		}
	}
}
