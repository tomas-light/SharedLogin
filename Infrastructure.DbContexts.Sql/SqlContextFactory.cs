namespace Infrastructure.DbContexts.Sql
{
	using Infrastructure.DbContexts;

	public class SqlContextFactory : IDbContextFactory
	{
		public BaseDbContext Create(IDbConfiguration dbConfiguration)
		{
			var dbContextOptions = SqlDbContextOptionsFactory.Create(dbConfiguration);
			var context = new SqlDbContext(dbContextOptions);
			return context;
		}
	}
}
