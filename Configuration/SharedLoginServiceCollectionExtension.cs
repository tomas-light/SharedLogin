namespace Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

	using Configuration.Dependencies;
    using Configuration.Dependencies.Strategies;

    public static class SharedLoginServiceCollectionExtension
	{
		public static IServiceCollection AddSharedLogin(
			this IServiceCollection services, 
			IConfiguration configuration, 
			string dbConnectionString, 
			DbConfigurationOptions dbConfigurationOptions = DbConfigurationOptions.Sql)
		{
			var strategyFactory = new DbContextDependenciesStrategyFactory();
			var strategy = strategyFactory.Make(dbConfigurationOptions);

			var dbContextFactory = strategy.GetContextFactory();
			var repositoryModule = strategy.GetDependenciesModule();

			var context = dbContextFactory.Create(dbConnectionString);

			var dbContextConfigurator = new DbContextConfigurator();
			dbContextConfigurator.Configure(context);

			var dependencyConfigurator = new DependencyConfigurator();
			dependencyConfigurator.Configure(services, repositoryModule);

			return services;
		}
	}
}
