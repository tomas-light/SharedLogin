namespace SharedLogin.Configuration
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using SharedLogin.Infrastructure.Contexts;

    public static class ContextConfiguration
	{
		public static void Configure(IServiceCollection services, string connectionString)
		{
			var factory = new SqlContextFactory(connectionString);
			var context = factory.CreateDbContext();
			
			//context.Database.EnsureCreated();
			context.Database.Migrate();

			//services.AddScoped<SqlDbContext>((serviceProvider) => context);

			services.AddScoped<SqlDbContext>(serviceProvider => new SqlContextFactory(connectionString).CreateDbContext());

			//services.AddSingleton<SqlContextFactory>((serviceProvider) => connectionString);

			//services.AddScoped<Func<string, SqlDbContext>>((serviceProvider) => );
		}
	}
}
