namespace Infrastructure.DbContexts.Sql
{
	using Microsoft.EntityFrameworkCore;
	using Infrastructure.DbContexts;

	public class SqlContextFactory : IDbContextFactory
	{
		public BaseDbContext Create(string connectionString)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext>();
			builder.UseSqlServer(connectionString);
			var context = new SqlDbContext(builder.Options);
			return context;
		}
	}
}
