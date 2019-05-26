namespace Infrastructure.Repositories.Histories
{
	using Infrastructure.DbContexts.PostgreSql;

	public class HistoryPostgreSqlRepository : BaseHistoryRepository
	{
		public HistoryPostgreSqlRepository(PostgreSqlDbContext dbContext)
			:base(dbContext)
		{
		}
	}
}
