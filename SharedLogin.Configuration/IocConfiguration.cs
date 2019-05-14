namespace SharedLogin.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
	using SharedLogin.Infrastructure.Repositories;
	using SharedLogin.Infrastructure.Repositories.Sql.Master;
    using SharedLogin.Services;
    using SharedLogin.Services.Core;
    using SharedLogin.Services.Core.Claims;

    public static class IocConfiguration
	{
		public static void Configure(IServiceCollection services)
		{
			RegisterRepository(services);
			RegisterServices(services);
			RegisterAuth(services);
		}

		public static void RegisterRepository(IServiceCollection services)
		{
			services.AddScoped<ISharedAccountsRepository, SharedAccountsRepository>();
			services.AddScoped<IAccessHistoryRepository, AccessHistoryRepository>();
		}

		public static void RegisterServices(IServiceCollection services)
		{
			services.AddScoped<ISharedAccountsService, SharedAccountsService>();
			services.AddScoped<IAccessHistoryService, AccessHistoryService>();
		}

		public static void RegisterAuth(IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, LoginClaimsPrincipalFactory>();
		}
	}
}
