namespace SharedLogin.Infrastructure.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
	using System.Reflection;

    public class SqlDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqlDbContext>
	{
		public SqlDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext>();
			builder.UseSqlServer(
				"Server=(localdb)\\mssqllocaldb;Database=<My_db_name>;Trusted_Connection=True;MultipleActiveResultSets=true",
				optionsBuilder =>
					optionsBuilder.MigrationsAssembly(typeof(SqlDbContext).GetTypeInfo().Assembly.GetName().Name)
			);
			return new SqlDbContext(builder.Options);
		}
	}
}
