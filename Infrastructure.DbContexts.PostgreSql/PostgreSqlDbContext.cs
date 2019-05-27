namespace Infrastructure.DbContexts.PostgreSql
{
    using Infrastructure.DbContexts.PostgreSql.ModelBuilders;
	using Microsoft.EntityFrameworkCore;

	public class PostgreSqlDbContext : BaseDbContext
	{
		public PostgreSqlDbContext(DbContextOptions<BaseDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder
				.BuildAccounts()
				.BuildHistories();
		}
	}
}
