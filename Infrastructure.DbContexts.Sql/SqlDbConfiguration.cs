namespace Infrastructure.DbContexts.Sql
{
	public class SqlDbConfiguration : IDbConfiguration
	{
		public string Server { get; set; }

		public string Database { get; set; }

		public bool IsTrastedConnection { get; set; }

		public bool IsMultipleActiveResultSets { get; set; }

		public string GetConnectionString()
		{
			return $"Server={Server};Database={Database};Trusted_Connection={IsTrastedConnection};MultipleActiveResultSets={IsMultipleActiveResultSets}";
		}
	}
}
