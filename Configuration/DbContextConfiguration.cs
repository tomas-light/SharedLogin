namespace Configuration
{
    using Infrastructure.DbContexts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Collections.Generic;
	using System.Text;

	abstract class DbContextConfiguration
	{
		public static void Configure(IServiceCollection services, string connectionString, DbConfigurationOptions dbConfigurationOptions)
		{
			BaseDbContext context;

			switch (dbConfigurationOptions)
			{
				case DbConfigurationOptions.PostgreSql:
					context = CreatePostgreSqlContext(connectionString);
					services.AddScoped(serviceProvider => CreatePostgreSqlContext(connectionString));
					break;

				case DbConfigurationOptions.Sql:
				default:
					context = CreateSqlContext(connectionString);
					services.AddScoped(serviceProvider => CreateSqlContext(connectionString));
					break;
			}

			context.Database.EnsureCreated();
			context.Database.Migrate();
		}

		private static SqlDbContext CreateSqlContext(string connectionString)
		{
			var factory = new SqlContextFactory(connectionString);
			return factory.CreateDbContext();
		}

		private static PostgreSqlDbContext CreatePostgreSqlContext(string connectionString)
		{
			var factory = new PostgreSqlContextFactory(connectionString);
			return factory.CreateDbContext();
		}
	}
}
