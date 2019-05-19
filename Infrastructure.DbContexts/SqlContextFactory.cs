namespace Infrastructure.DbContexts
{
	using Microsoft.EntityFrameworkCore;
    using System;

    public class SqlContextFactory<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		private readonly string connectionString;

		public SqlContextFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public SqlDbContext<TUserPrimaryKey> CreateDbContext()
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext<TUserPrimaryKey>>();
			builder.UseSqlServer(this.connectionString);
			var context = new SqlDbContext<TUserPrimaryKey>(builder.Options);
			return context;
		}
	}
}
