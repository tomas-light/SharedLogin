namespace Infrastructure.Repositories.Accounts
{
	using Infrastructure.DbContexts.Sql;

	public class AccountSqlRepository : BaseAccountRepository
	{
		public AccountSqlRepository(SqlDbContext dbContext)
			:base(dbContext)
		{
		}
	}
}
