using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts
{
	public class SqlDesignTimeDbContext : SqlDbContext<string>
	{
		public SqlDesignTimeDbContext(DbContextOptions<BaseDbContext<string>> options)
			: base(options)
		{
		}
	}
}
