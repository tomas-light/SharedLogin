namespace Infrastructure.DbContexts
{
	using Microsoft.EntityFrameworkCore;
    using System;

    public class PostgreSqlContextFactory
	{
		private readonly string connectionString;

		public PostgreSqlContextFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public PostgreSqlDbContext CreateDbContext()
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext>();
			builder.UseNpgsql(this.connectionString);
			var context = new PostgreSqlDbContext(builder.Options);
			return context;
		}
	}
}
