namespace Infrastructure.DbContexts
{
	using Microsoft.EntityFrameworkCore;
    using System;

    public class SqlContextFactory
	{
		private readonly string connectionString;

		public SqlContextFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public SqlDbContext CreateDbContext()
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext>();
			builder.UseSqlServer(this.connectionString);
			var context = new SqlDbContext(builder.Options);
			return context;
		}
	}
}
