namespace Infrastructure.DbContexts.PostgreSql
{
	using Microsoft.EntityFrameworkCore;

    public class PostgreSqlContextCreator : IDbContextCreator
	{
		public BaseDbContext Create(string connectionString)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext>();
			builder.UseNpgsql(connectionString);
			var context = new PostgreSqlDbContext(builder.Options);
			return context;
		}
	}
}
