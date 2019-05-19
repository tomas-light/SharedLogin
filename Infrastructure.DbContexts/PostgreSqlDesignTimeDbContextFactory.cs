namespace Infrastructure.DbContexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
	using System;
    using System.Reflection;

    class PostgreSqlDesignTimeDbContextFactory<TUserPrimaryKey> : IDesignTimeDbContextFactory<PostgreSqlDbContext<TUserPrimaryKey>>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public PostgreSqlDbContext<TUserPrimaryKey> CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext<TUserPrimaryKey>>();
			builder.UseNpgsql(
				"User ID=postgres;Password=root;Host=localhost;Port=5432;Database=<Your_Database_Name>;",
				optionsBuilder =>
					optionsBuilder.MigrationsAssembly(typeof(PostgreSqlDbContext<TUserPrimaryKey>).GetTypeInfo().Assembly.GetName().Name)
			);
			return new PostgreSqlDbContext<TUserPrimaryKey>(builder.Options);
		}
	}
}
