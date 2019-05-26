namespace Configuration
{
    using Microsoft.Extensions.DependencyInjection;
	using System;

	using Configuration.Dependencies;
    using Configuration.Dependencies.Strategies;
    using Infrastructure.DbContexts;

    public static class SharedLoginServiceCollectionExtension
	{
		public static IServiceProvider AddSharedLogin(
			this IServiceCollection services,
			IDbConfiguration dbConfiguration, 
			DbConfigurationOptions dbConfigurationOptions = DbConfigurationOptions.Sql)
		{
			var strategyFactory = new DbContextDependenciesStrategyFactory();
			var strategy = strategyFactory.Make(dbConfigurationOptions);

			var dbContextFactory = strategy.GetContextFactory();
			var repositoryModule = strategy.GetDependenciesModule(dbConfiguration);

			var context = dbContextFactory.Create(dbConfiguration);

			var dbContextConfigurator = new DbContextConfigurator();
			dbContextConfigurator.Configure(context);

			var dependencyConfigurator = new DependencyConfigurator();
			var serviceProvider = dependencyConfigurator.Configure(services, repositoryModule);

			return serviceProvider;
		}
	}
}
