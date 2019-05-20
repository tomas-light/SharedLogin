namespace WebApp.Data
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Design;
	using System.Reflection;

	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
			builder.UseNpgsql(
				"User ID=postgres;Password=root;Host=localhost;Port=5432;Database=<Your_Database_Name>;",
				optionsBuilder =>
					optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name)
			);
			return new ApplicationDbContext(builder.Options);
		}
	}
}
