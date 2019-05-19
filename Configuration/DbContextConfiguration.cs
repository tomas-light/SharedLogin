namespace Configuration
{
    using Infrastructure.DbContexts;
    using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Collections.Generic;
	using System.Text;

	abstract class DbContextConfiguration<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public static void Configure(IServiceCollection services, string connectionString)
		{
			var context = CreateContext(connectionString);
			//context.Database.EnsureCreated();
			context.Database.Migrate();

			services.AddScoped<SqlDbContext<TUserPrimaryKey>>(serviceProvider => CreateSqlContext(connectionString));
			services.AddScoped<PostgreSqlDbContext<TUserPrimaryKey>>(serviceProvider => CreatePostgreSqlContext(connectionString));

			//services.AddScoped<Func<string, SqlDbContext>>((serviceProvider) => );
		}

		private static SqlDbContext<TUserPrimaryKey> CreateSqlContext(string connectionString)
		{
			var factory = new SqlContextFactory(connectionString);
			return factory.CreateDbContext();
		}

		private static PostgreSqlDbContext<TUserPrimaryKey> CreatePostgreSqlContext(string connectionString)
		{
			var factory = new PostgreSqlContextFactory(connectionString);
			return factory.CreateDbContext();
		}
	}
}
