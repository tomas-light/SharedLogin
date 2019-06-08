namespace WebApp.AppConfiguration.DbContext
{
	using Infrastructure.DbContexts;
    using Infrastructure.DbContexts.PostgreSql;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

    public class PostgreSqlDbContextConfigurationStrategy : IDbContextConfigurationStrategy
	{
		private IConfiguration configuration;

		public PostgreSqlDbContextConfigurationStrategy(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void AddDbContext<TDbContext>(IServiceCollection services)
			where TDbContext : DbContext
		{
			services
				.AddEntityFrameworkNpgsql()
				.AddDbContext<TDbContext>(optionBuilder =>
					optionBuilder.UseNpgsql(
						configuration.GetConnectionString("PostgreSqlConnection")));
		}

		public IDbConfiguration GetConfiguration()
		{
			var dbConfiguration = new PostgreSqlDbConfiguration
			{
				UserId = configuration.GetValue<string>("ConnectionStrings:PostgreSql:User ID"),
				Password = configuration.GetValue<string>("ConnectionStrings:PostgreSql:Password"),
				Host = configuration.GetValue<string>("ConnectionStrings:PostgreSql:Host"),
				Port = configuration.GetValue<string>("ConnectionStrings:PostgreSql:Port"),
				Database = configuration.GetValue<string>("ConnectionStrings:PostgreSql:Database"),
			};
			return dbConfiguration;
		}
	}
}
