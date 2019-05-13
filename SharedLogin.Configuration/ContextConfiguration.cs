namespace SharedLogin.Configuration
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using SharedLogin.Infrastructure.Contexts;

    public static class ContextConfiguration
	{
		public static void Configure(IServiceCollection services, string connectionString)
		{
			var context = CreateContext(connectionString);			
			//context.Database.EnsureCreated();
			context.Database.Migrate();

			services.AddScoped<SqlDbContext>(serviceProvider => CreateContext(connectionString));

			//services.AddScoped<Func<string, SqlDbContext>>((serviceProvider) => );
		}

		private static SqlDbContext CreateContext(string connectionString)
		{
			var factory = new SqlContextFactory(connectionString);
			return factory.CreateDbContext();
		}
	}
}
