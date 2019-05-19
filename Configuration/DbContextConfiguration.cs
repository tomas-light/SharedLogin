namespace Configuration
{
    using Infrastructure.DbContexts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
	using System;

	abstract class DbContextConfiguration<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public static void Configure(IServiceCollection services, string connectionString, DbConfigurationOptions dbConfigurationOptions)
		{
			BaseDbContext<TUserPrimaryKey> context;

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

		private static SqlDbContext<TUserPrimaryKey> CreateSqlContext(string connectionString)
		{
			var factory = new SqlContextFactory<TUserPrimaryKey>(connectionString);
			return factory.CreateDbContext();
		}

		private static PostgreSqlDbContext<TUserPrimaryKey> CreatePostgreSqlContext(string connectionString)
		{
			var factory = new PostgreSqlContextFactory<TUserPrimaryKey>(connectionString);
			return factory.CreateDbContext();
		}
	}
}
