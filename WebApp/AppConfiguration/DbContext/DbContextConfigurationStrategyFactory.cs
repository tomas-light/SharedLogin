namespace WebApp.AppConfiguration.DbContext
{
    using Configuration.Db;
    using Microsoft.Extensions.Configuration;

    public class DbContextConfigurationStrategyFactory
	{
		private IConfiguration configuration;

		public DbContextConfigurationStrategyFactory(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public IDbContextConfigurationStrategy Make(DbConfigurationOptions dbConfigurationOptions = DbConfigurationOptions.Sql)
		{
			switch(dbConfigurationOptions)
			{
				case DbConfigurationOptions.PostgreSql:
					return new PostgreSqlDbContextConfigurationStrategy(configuration);

				case DbConfigurationOptions.Sql:
				default:
					return new SqlDbContextConfigurationStrategy(configuration);
			}
		}
	}
}
