namespace Configuration.Dependencies
{
    using Autofac;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;

	using Core.Services.Claims;
    using Infrastructure.DbContexts.Sql;
    using Infrastructure.DbContexts.PostgreSql;

    internal class DependencyConfigurator
	{
		public void Configure(IServiceCollection services)
		{
			var builder = new ContainerBuilder();

			var mapper = MapperConfigurator.Configure();
			builder.RegisterInstance(mapper).As<IMapper>();

			//services.AddDbContext(dbContextFactory.Create(dbConnectionString));
			//services.AddScoped(serviceProvider => dbContextFactory.Create(dbConnectionString));
			//builder.RegisterInstance(dbContextFactory.Create(dbConnectionString)).AsSelf();
			builder.RegisterType<SqlDbContext>().AsSelf();
			builder.RegisterType<PostgreSqlDbContext>().AsSelf();

			builder.RegisterRepositories();
			builder.RegisterServices();

			//services.AddHttpContextAccessor();
			builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
			builder.RegisterType<ClaimsPrincipalFactory>().As<IUserClaimsPrincipalFactory<IdentityUser>>();
		}
	}
}
