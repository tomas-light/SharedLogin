namespace SharedLogin.Infrastructure.Contexts
{
    using Microsoft.EntityFrameworkCore;

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
