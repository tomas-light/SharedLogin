namespace Infrastructure.DbContexts.PostgreSql
{
    public class PostgreSqlContextFactory : IDbContextFactory
	{
		public BaseDbContext Create(IDbConfiguration dbConfiguration)
		{
			var dbContextOptions = PostgreSqlDbContextOptionsFactory.Create(dbConfiguration);
			var context = new PostgreSqlDbContext(dbContextOptions);
			return context;
		}
	}
}
