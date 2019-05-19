namespace Configuration
{
    using Core.Services;
    using Core.Services.Accounts;
    using Core.Services.Claims;
    using Core.Services.Histories;
    using Infrastructure.Repositories;
    using Infrastructure.Repositories.Accounts;
    using Infrastructure.Repositories.Histories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;

	abstract class DependencyConfiguration
	{
		public static void Configure(IServiceCollection services, DbConfigurationOptions dbConfigurationOptions)
		{
			switch(dbConfigurationOptions)
			{
				case DbConfigurationOptions.PostgreSql:
					RegisterPostgreSqlRepository(services);
					break;

				case DbConfigurationOptions.Sql:
				default:
					RegisterSqlRepository(services);
					break;
			}

			RegisterServices(services);
			RegisterAuth(services);
		}

		public static void RegisterPostgreSqlRepository(IServiceCollection services)
		{
			services.AddScoped<IAccountRepository, AccountPostgreSqlRepository>();
			services.AddScoped<IHistoryRepository, HistoryPostgreSqlRepository>();
		}

		public static void RegisterSqlRepository(IServiceCollection services)
		{
			services.AddScoped<IAccountRepository, AccountSqlRepository>();
			services.AddScoped<IHistoryRepository, HistorySqlRepository>();
		}

		public static void RegisterServices(IServiceCollection services)
		{
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IHistoryService, HistoryService>();
		}

		public static void RegisterAuth(IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, ClaimsPrincipalFactory>();
		}
	}
}
