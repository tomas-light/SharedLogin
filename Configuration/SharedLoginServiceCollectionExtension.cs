namespace Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

	using Configuration.DbContexts;
	using Configuration.Dependencies;

	public static class SharedLoginServiceCollectionExtension
	{
		public static IServiceCollection AddSharedLogin(
			this IServiceCollection services, 
			IConfiguration configuration, 
			string dbConnectionString, 
			DbConfigurationOptions dbConfigurationOptions = DbConfigurationOptions.Sql)
		{
			var dbContextCreatorFactory = new DbContextCreatorFactory();
			var dbContextCreator = dbContextCreatorFactory.Make(dbConfigurationOptions);

			var context = dbContextCreator.Create(dbConnectionString);

			var dbContextConfigurator = new DbContextConfigurator();
			dbContextConfigurator.Configure(context);

			var dependencyConfigurator = new DependencyConfigurator();
			dependencyConfigurator.Configure(services);

			return services;
		}
	}
}
