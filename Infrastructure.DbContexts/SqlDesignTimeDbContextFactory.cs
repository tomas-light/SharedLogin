namespace Infrastructure.DbContexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
	using System;
    using System.Reflection;

    class SqlDesignTimeDbContextFactory<TUserPrimaryKey> : IDesignTimeDbContextFactory<SqlDbContext<TUserPrimaryKey>>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public SqlDbContext<TUserPrimaryKey> CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext<TUserPrimaryKey>>();
			builder.UseSqlServer(
				"Server=(localdb)\\mssqllocaldb;Database=<My_db_name>;Trusted_Connection=True;MultipleActiveResultSets=true",
				optionsBuilder =>
					optionsBuilder.MigrationsAssembly(typeof(SqlDbContext<TUserPrimaryKey>).GetTypeInfo().Assembly.GetName().Name)
			);
			return new SqlDbContext<TUserPrimaryKey>(builder.Options);
		}
	}
}
