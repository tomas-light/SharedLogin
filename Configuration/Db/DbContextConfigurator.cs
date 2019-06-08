namespace Configuration.Db
{
    using Autofac.Core;
    using Configuration.Dependencies.Strategies;
    using Infrastructure.DbContexts;
    using Microsoft.EntityFrameworkCore;

	public class DbContextConfigurator
	{
		public static BaseDbContext CreateDbContext(
			IDbConfiguration dbConfiguration,
			DbConfigurationOptions dbConfigurationOptions)
		{
			var strategyFactory = new DbContextDependenciesStrategyFactory();
			var strategy = strategyFactory.Make(dbConfigurationOptions);

			var dbContextFactory = strategy.GetContextFactory();
			var context = dbContextFactory.Create(dbConfiguration);

			return context;
		}

		public static IModule GetDbContextDependencies(
			IDbConfiguration dbConfiguration,
			DbConfigurationOptions dbConfigurationOptions)
		{
			var strategyFactory = new DbContextDependenciesStrategyFactory();
			var strategy = strategyFactory.Make(dbConfigurationOptions);

			var repositoryModule = strategy.GetDependenciesModule(dbConfiguration);

			return repositoryModule;
		}

		public static void InitDbContext(DbContext dbContext)
		{
			dbContext.Database.Migrate();
		}
	}
}
