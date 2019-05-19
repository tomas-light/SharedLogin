namespace Infrastructure.DbContexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
	using System;
    using System.Reflection;

    class PostgreSqlDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlDesignTimeDbContext>
	{
		public PostgreSqlDesignTimeDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext<string>>();
			builder.UseNpgsql(
				"User ID=postgres;Password=root;Host=localhost;Port=5432;Database=<Your_Database_Name>;",
				optionsBuilder =>
					optionsBuilder.MigrationsAssembly(typeof(PostgreSqlDesignTimeDbContext).GetTypeInfo().Assembly.GetName().Name)
			);
			return new PostgreSqlDesignTimeDbContext(builder.Options);
		}
	}
}
