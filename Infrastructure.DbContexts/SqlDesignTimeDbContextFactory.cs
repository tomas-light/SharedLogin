namespace Infrastructure.DbContexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
	using System;
    using System.Reflection;

    class SqlDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqlDesignTimeDbContext>
	{
		public SqlDesignTimeDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext<string>>();
			builder.UseSqlServer(
				"Server=(localdb)\\mssqllocaldb;Database=<My_db_name>;Trusted_Connection=True;MultipleActiveResultSets=true",
				optionsBuilder =>
					optionsBuilder.MigrationsAssembly(typeof(SqlDesignTimeDbContext).GetTypeInfo().Assembly.GetName().Name)
			);
			return new SqlDesignTimeDbContext(builder.Options);
		}
	}
}
