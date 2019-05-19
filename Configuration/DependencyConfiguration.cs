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

	abstract class DependencyConfiguration<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
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

		public static void RegisterSqlRepository(IServiceCollection services)
		{
			services.AddScoped<IAccountRepository<TUserPrimaryKey>, AccountSqlRepository<TUserPrimaryKey>>();
			services.AddScoped<IHistoryRepository<TUserPrimaryKey>, HistorySqlRepository<TUserPrimaryKey>>();
		}

		public static void RegisterPostgreSqlRepository(IServiceCollection services)
		{
			services.AddScoped<IAccountRepository<TUserPrimaryKey>, AccountPostgreSqlRepository<TUserPrimaryKey>>();
			services.AddScoped<IHistoryRepository<TUserPrimaryKey>, HistoryPostgreSqlRepository<TUserPrimaryKey>>();
		}

		public static void RegisterServices(IServiceCollection services)
		{
			services.AddScoped<IAccountService<TUserPrimaryKey>, AccountService<TUserPrimaryKey>>();
			services.AddScoped<IHistoryService<TUserPrimaryKey>, HistoryService<TUserPrimaryKey>>();
		}

		public static void RegisterAuth(IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser<TUserPrimaryKey>>, ClaimsPrincipalFactory<TUserPrimaryKey>>();
		}
	}
}
