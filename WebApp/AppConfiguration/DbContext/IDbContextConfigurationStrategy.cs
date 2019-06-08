namespace WebApp.AppConfiguration.DbContext
{
    using Infrastructure.DbContexts;
	using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

	public interface IDbContextConfigurationStrategy
	{
		IDbConfiguration GetConfiguration();

		void AddDbContext<TDbContext>(IServiceCollection services)
			where TDbContext : DbContext;
	}
}
