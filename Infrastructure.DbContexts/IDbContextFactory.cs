namespace Infrastructure.DbContexts
{
	public interface IDbContextFactory
	{
		BaseDbContext Create(IDbConfiguration dbConfiguration);
	}
}
