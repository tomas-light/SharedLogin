namespace Configuration.Dependencies.Strategies
{
	using Autofac.Core;

    using Configuration.Dependencies.Repositories;
    using Infrastructure.DbContexts;
    using Infrastructure.DbContexts.Sql;

    internal class SqlRepositoryDependenciesStrategy : IDbContextDependenciesStrategy
	{
		public IDbContextFactory GetContextFactory()
		{
			return new SqlContextFactory();
		}

		public IModule GetDependenciesModule()
		{
			return new SqlModule();
		}
	}
}
