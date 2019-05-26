namespace Infrastructure.DbContexts
{
	public interface IDbConfiguration
	{
		string GetConnectionString();
	}
}
