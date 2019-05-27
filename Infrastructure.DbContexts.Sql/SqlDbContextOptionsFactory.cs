namespace Infrastructure.DbContexts.Sql
{
	using Microsoft.EntityFrameworkCore;

	public static class SqlDbContextOptionsFactory
	{
		public static DbContextOptions<BaseDbContext> Create(IDbConfiguration dbConfiguration)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext>();
			var connectionString = dbConfiguration.GetConnectionString();
			builder.UseSqlServer(connectionString);
			return builder.Options;
		}
	}
}
