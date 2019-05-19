namespace Infrastructure.DbContexts
{
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
	using System;

	public class BaseDbContext<TUserPrimaryKey> : DbContext
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public BaseDbContext(DbContextOptions<BaseDbContext<TUserPrimaryKey>> options)
			: base(options)
		{
		}

		public DbSet<Account<TUserPrimaryKey>> Accounts { get; set; }

		public DbSet<History<TUserPrimaryKey>> Histories { get; set; }
	}
}
