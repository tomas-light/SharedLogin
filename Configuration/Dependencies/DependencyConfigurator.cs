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

    internal class DependencyConfigurator
	{
		public IServiceProvider Configure(IServiceCollection services, IModule repositoryModule)
		{
			var builder = new ContainerBuilder();

			var mapper = MapperConfigurator.Configure();
			builder.RegisterInstance(mapper).As<IMapper>();

			builder.RegisterModule(repositoryModule);

			builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
			builder.RegisterType<ClaimsPrincipalFactory>().As<IUserClaimsPrincipalFactory<IdentityUser>>();
			builder.RegisterType<UserManager<IdentityUser>>().AsSelf();
			builder.RegisterType<RoleManager<IdentityRole>>().AsSelf();

			builder.RegisterServices();
			builder.Populate(services);
			var applicationContainer = builder.Build();

			return new AutofacServiceProvider(applicationContainer);
		}
	}
}
