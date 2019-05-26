namespace Infrastructure.DbContexts.PostgreSql
{
	using Microsoft.EntityFrameworkCore;

	public static class PostgreSqlDbContextOptionsFactory
	{
		public static DbContextOptions<BaseDbContext> Create(IDbConfiguration dbConfiguration)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext>();
			var connectionString = dbConfiguration.GetConnectionString();
			builder.UseNpgsql(connectionString);
			return builder.Options;
		}
	}
}
