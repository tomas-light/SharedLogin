namespace Configuration.Dependencies
{
    using Autofac;
	using Autofac.Core;
	using Autofac.Extensions.DependencyInjection;
	using AutoMapper;
    using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;
	using System;

	using Core.Services.Claims;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    internal class DependencyConfigurator
	{
		public IServiceProvider Configure<TContext, TUser, TRole, TKey>(IServiceCollection services, IModule repositoryModule)
			where TContext : IdentityDbContext<IdentityUser<TKey>, IdentityRole<TKey>, TKey>
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TKey : IEquatable<TKey>
		{
			var builder = new ContainerBuilder();

			var mapper = MapperConfigurator.Configure();
			builder.RegisterInstance(mapper).As<IMapper>();

			builder.RegisterModule(repositoryModule);

			builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

			builder.RegisterType<UserStore<TUser, TRole, TContext, TKey>>().As<IUserStore<TUser>>();
			builder.RegisterType<RoleStore<TRole, TContext, TKey>>().As<IRoleStore<TRole>>();
			builder.RegisterType<ClaimsPrincipalFactory>().As<IUserClaimsPrincipalFactory<TUser>>();
			builder.RegisterType<UserManager<TUser>>().AsSelf();
			builder.RegisterType<RoleManager<TRole>>().AsSelf();

			builder.RegisterServices();
			builder.Populate(services);
			var applicationContainer = builder.Build();

			return new AutofacServiceProvider(applicationContainer);
		}
	}
}
