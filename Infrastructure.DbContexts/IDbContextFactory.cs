namespace Infrastructure.DbContexts
{
	public interface IDbContextFactory
	{
		BaseDbContext Create(string connectionString);
	}
}
