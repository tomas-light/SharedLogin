namespace Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

	public static class Configger<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public static void Configure(IServiceCollection services, IConfiguration configuration, string dbConnectionString, DbConfigurationOptions dbConfigurationOptions = DbConfigurationOptions.Sql)
		{
			DependencyConfiguration<TUserPrimaryKey>.Configure(services, dbConfigurationOptions);
			DbContextConfiguration<TUserPrimaryKey>.Configure(services, dbConnectionString, dbConfigurationOptions);
		}
	}
}
