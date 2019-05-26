namespace Infrastructure.DbContexts.PostgreSql
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Design;
	using System.Reflection;

	class PostgreSqlDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlDbContext>
	{
		public PostgreSqlDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext>();
			builder.UseNpgsql(
				"User ID=postgres;Password=root;Host=localhost;Port=5432;Database=<Your_Database_Name>;",
				optionsBuilder =>
					optionsBuilder.MigrationsAssembly(typeof(PostgreSqlDbContext).GetTypeInfo().Assembly.GetName().Name)
			);
			return new PostgreSqlDbContext(builder.Options);
		}
	}
}
