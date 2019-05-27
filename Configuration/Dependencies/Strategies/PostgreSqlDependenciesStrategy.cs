namespace Configuration.Dependencies.Strategies
{
	using Autofac.Core;

    using Configuration.Dependencies.Repositories;
    using Infrastructure.DbContexts;
    using Infrastructure.DbContexts.PostgreSql;

    internal class PostgreSqlRepositoryDependenciesStrategy : IDbContextDependenciesStrategy
	{
		public IDbContextFactory GetContextFactory()
		{
			return new PostgreSqlContextFactory();
		}

		public IModule GetDependenciesModule(IDbConfiguration dbConfiguration)
		{
			return new PostgreSqlModule(dbConfiguration);
		}
	}
}
