namespace Infrastructure.DbContexts
{
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
	using System;

	public class BaseDbContext : DbContext
	{
		public BaseDbContext(DbContextOptions<BaseDbContext> options)
			: base(options)
		{
		}

		public DbSet<Account> Accounts { get; set; }

		public DbSet<History> Histories { get; set; }
	}
}
