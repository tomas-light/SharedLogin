namespace Configuration.Dependencies.Strategies
{
    using Autofac.Core;
    using Infrastructure.DbContexts;

	internal interface IDbContextDependenciesStrategy
	{
		IDbContextFactory GetContextFactory();

		IModule GetDependenciesModule(IDbConfiguration dbConfiguration);
	}
}
