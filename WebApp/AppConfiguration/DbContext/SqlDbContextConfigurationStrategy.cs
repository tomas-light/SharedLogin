namespace WebApp.AppConfiguration.DbContext
{
	using Infrastructure.DbContexts;
    using Infrastructure.DbContexts.Sql;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

    public class SqlDbContextConfigurationStrategy : IDbContextConfigurationStrategy
	{
		private IConfiguration configuration;

		public SqlDbContextConfigurationStrategy(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void AddDbContext<TDbContext>(IServiceCollection services)
			where TDbContext : DbContext
		{
			services.AddDbContext<TDbContext>(optionBuilder =>
				optionBuilder.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection")));
		}

		public IDbConfiguration GetConfiguration()
		{
			var dbConfiguration = new SqlDbConfiguration
			{
				Database = configuration.GetValue<string>("ConnectionStrings:Sql:Database"),
				Server = configuration.GetValue<string>("ConnectionStrings:Sql:Server"),
				IsMultipleActiveResultSets = configuration.GetValue<bool>("ConnectionStrings:Sql:MultipleActiveResultSets"),
				IsTrastedConnection = configuration.GetValue<bool>("ConnectionStrings:Sql:Trusted_Connection"),
			};
			return dbConfiguration;
		}
	}
}
