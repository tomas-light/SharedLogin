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
			Func<string, BaseDbContext> createContext;

			switch (dbConfigurationOptions)
			{
				case DbConfigurationOptions.PostgreSql:
					createContext = CreatePostgreSqlContext;
					break;

				case DbConfigurationOptions.Sql:
				default:
					createContext = CreateSqlContext;
					break;
			}

			context = createContext(connectionString);
			//context.Database.EnsureCreated();
			context.Database.Migrate();

			services.AddScoped(serviceProvider => createContext(connectionString));

			//services.AddScoped<Func<string, SqlDbContext>>((serviceProvider) => );
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
