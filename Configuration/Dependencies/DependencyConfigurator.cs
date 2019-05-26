namespace Configuration.Dependencies
{
    using Autofac;
	using Autofac.Core;
	using AutoMapper;
    using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;

	using Core.Services.Claims;

    internal class DependencyConfigurator
	{
		public void Configure(IServiceCollection services, IModule repositoryModule)
		{
			var builder = new ContainerBuilder();

			var mapper = MapperConfigurator.Configure();
			builder.RegisterInstance(mapper).As<IMapper>();

			builder.RegisterModule(repositoryModule);

			builder.RegisterServices();

			builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
			builder.RegisterType<ClaimsPrincipalFactory>().As<IUserClaimsPrincipalFactory<IdentityUser>>();
		}
	}
}
