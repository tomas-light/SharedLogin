namespace Infrastructure.DbContexts.PostgreSql
{
	public class PostgreSqlDbConfiguration : IDbConfiguration
	{
		public string UserId { get; set; }

		public string Password { get; set; }

		public string Host { get; set; }

		public string Port { get; set; }

		public string Database { get; set; }

		public string GetConnectionString()
		{
			return $"User ID={UserId};Password={Password};Host={Host};Port={Port};Database={Database};";
		}
	}
}
