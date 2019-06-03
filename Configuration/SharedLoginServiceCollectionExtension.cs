namespace Configuration
{
	using Microsoft.Extensions.DependencyInjection;
	using System;

	using Configuration.Dependencies;
	using Configuration.Dependencies.Strategies;
	using Infrastructure.DbContexts;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Autofac;
    using AutoMapper;
    using Autofac.Core;

    public static class SharedLoginServiceCollectionExtension
	{
		public static IServiceProvider AddSharedLogin<TContext, TUser, TRole, TKey>(
			this IServiceCollection services,
			Action<IMapperConfigurationExpression> mapperExpression,
			IDbConfiguration dbConfiguration,
			DbConfigurationOptions dbConfigurationOptions = DbConfigurationOptions.Sql)
			where TContext : IdentityDbContext<TUser, TRole, TKey>
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TKey : IEquatable<TKey>
		{
			var repositoryModule = ConfigureDbContext(dbConfiguration, dbConfigurationOptions);

			var dependencyConfigurator = new DependencyConfigurator();
			var serviceProvider = dependencyConfigurator
										.CreateConfiguredServiceProvider<TContext, TUser, TRole, TKey>(
											services, 
											repositoryModule, 
											mapperExpression);

			return serviceProvider;
		}

		public static void AddSharedLoginServices<TContext, TUser, TRole, TKey>(
			this ContainerBuilder containerBuilder,
			Action<IMapperConfigurationExpression> mapperExpression,
			IDbConfiguration dbConfiguration,
			DbConfigurationOptions dbConfigurationOptions = DbConfigurationOptions.Sql)
			where TContext : IdentityDbContext<TUser, TRole, TKey>
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TKey : IEquatable<TKey>
		{
			var repositoryModule = ConfigureDbContext(dbConfiguration, dbConfigurationOptions);

			var dependencyConfigurator = new DependencyConfigurator();
			dependencyConfigurator.Registertypes<TContext, TUser, TRole, TKey>(
				containerBuilder, 
				repositoryModule, 
				mapperExpression);
		}

		private static IModule ConfigureDbContext(
			IDbConfiguration dbConfiguration,
			DbConfigurationOptions dbConfigurationOptions)
		{
			var strategyFactory = new DbContextDependenciesStrategyFactory();
			var strategy = strategyFactory.Make(dbConfigurationOptions);

			var dbContextFactory = strategy.GetContextFactory();
			var repositoryModule = strategy.GetDependenciesModule(dbConfiguration);

			var context = dbContextFactory.Create(dbConfiguration);

			var dbContextConfigurator = new DbContextConfigurator();
			dbContextConfigurator.Configure(context);

			return repositoryModule;
		}
	}
}
