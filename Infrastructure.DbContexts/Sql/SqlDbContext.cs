namespace Infrastructure.DbContexts
{
    using Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;

	public class SqlDbContext : BaseDbContext
	{
		public SqlDbContext(DbContextOptions<BaseDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			BuildSharedAccounts(modelBuilder);
			BuildAccessHistory(modelBuilder);
		}

		private static void BuildSharedAccounts(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>(account =>
			{
				account.ToTable("AccessibleAccounts");

				account.HasMany(sa => sa.AccessHistories)
					.WithOne(accessHistory => accessHistory.AccessibleAccount);
			});
		}

		private static void BuildAccessHistory(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<History>(history =>
			{
				history.ToTable("AccessibleAccountsHistories");

				history
					.HasOne(ah => ah.AccessibleAccount)
					.WithMany()
					.HasForeignKey(ah => ah.AccountId)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
