namespace Configuration
{
	using Autofac;
	using AutoMapper;
	using Autofac.Core;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using System;

	using Configuration.Db;
	using Configuration.Dependencies;
	using Configuration.Dependencies.Strategies;
	using Infrastructure.DbContexts;

    public static class SharedLoginServiceCollectionExtension
	{
		public static void AddSharedLoginDependecies<TContext, TUser, TRole, TKey>(
			this ContainerBuilder containerBuilder,
			Action<IMapperConfigurationExpression> mapperExpression,
			IModule repositoryDependenciesModule)
				where TContext : IdentityDbContext<TUser, TRole, TKey>
				where TUser : IdentityUser<TKey>
				where TRole : IdentityRole<TKey>
				where TKey : IEquatable<TKey>
		{
			var dependencyConfigurator = new DependencyConfigurator();
			dependencyConfigurator.Registertypes<TContext, TUser, TRole, TKey>(
				containerBuilder,
				repositoryDependenciesModule, 
				mapperExpression);
		}
	}
}
