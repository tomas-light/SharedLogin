namespace Infrastructure.Repositories.Accounts
{
	using Infrastructure.DbContexts.PostgreSql;

	public class AccountPostgreSqlRepository : BaseAccountRepository
	{
		public AccountPostgreSqlRepository(PostgreSqlDbContext dbContext)
			: base(dbContext)
		{
		}
	}
}
