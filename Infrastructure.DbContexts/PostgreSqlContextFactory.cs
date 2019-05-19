namespace Infrastructure.DbContexts
{
	using Microsoft.EntityFrameworkCore;
    using System;

    public class PostgreSqlContextFactory<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		private readonly string connectionString;

		public PostgreSqlContextFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public PostgreSqlDbContext<TUserPrimaryKey> CreateDbContext()
		{
			var builder = new DbContextOptionsBuilder<BaseDbContext<TUserPrimaryKey>>();
			builder.UseNpgsql(this.connectionString);
			var context = new PostgreSqlDbContext<TUserPrimaryKey>(builder.Options);
			return context;
		}
	}
}
