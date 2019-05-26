namespace Infrastructure.DbContexts
{
	public interface IDbContextCreator
	{
		BaseDbContext Create(string connectionString);
	}
}
