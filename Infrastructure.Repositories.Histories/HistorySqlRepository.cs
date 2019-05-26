namespace Infrastructure.Repositories.Histories
{
	using Infrastructure.DbContexts.Sql;

	public class HistorySqlRepository : BaseHistoryRepository
	{
		public HistorySqlRepository(SqlDbContext dbContext)
			:base(dbContext)
		{
		}
	}
}
