namespace Infrastructure.DbContexts
{
	using Microsoft.EntityFrameworkCore;

	public class PostgreSqlDesignTimeDbContext : PostgreSqlDbContext<string>
	{
		public PostgreSqlDesignTimeDbContext(DbContextOptions<BaseDbContext<string>> options)
			: base(options)
		{
		}
	}
}
