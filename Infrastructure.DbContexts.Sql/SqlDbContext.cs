namespace Infrastructure.DbContexts.Sql
{
    using Infrastructure.DbContexts.Sql.ModelBuilders;
    using Microsoft.EntityFrameworkCore;

	public class SqlDbContext : BaseDbContext
	{
		public SqlDbContext(DbContextOptions<BaseDbContext> options)
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
