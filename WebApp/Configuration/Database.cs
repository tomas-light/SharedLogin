namespace WebApp.Configuration
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using SharedLogin.Infrastructure.Contexts;
    using System.Reflection;
    using WebApp.Data;

	public static class Database
	{
		public static IServiceCollection AddDefaultDabaseConnection(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UserSqlWithDefaultConnections(configuration)
			);

			services.AddDbContext<SqlDbContext<string>>(options =>
				options.UserSqlWithDefaultConnections(configuration)
			);

			return services;
		}

		public static IApplicationBuilder CreateSharedLoginDataBase(
			this IApplicationBuilder app, 
			IConfiguration configuration)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext<string>>();
			//builder.UseSqlServer(
			//	configuration.GetDefaultConnectionString(),
			//	optionsBuilder => 
			//		optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name)
			//);
			builder.UseSqlServer(
				configuration.GetDefaultConnectionString(),
				ConfigureMigration
			);

			using (var context = new SqlDbContext<string>(builder.Options))
			{
				//context.Database.EnsureCreated();
				context.Database.Migrate();
			}
			return app;
		}

		private static DbContextOptionsBuilder UserSqlWithDefaultConnections(
			this DbContextOptionsBuilder builder, 
			IConfiguration configuration)
		{
			return builder.UseSqlServer(configuration.GetDefaultConnectionString());
		}

		private static string GetDefaultConnectionString(this IConfiguration configuration)
		{
			return configuration.GetConnectionString("DefaultConnection");
		}

		private static void ConfigureMigration(SqlServerDbContextOptionsBuilder sqlBuilder)
		{
			sqlBuilder.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
		}
	}
}
