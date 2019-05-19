namespace Infrastructure.DbContexts
{
	using Infrastructure.Models;
	using Microsoft.EntityFrameworkCore;
	using System;

	public class PostgreSqlDbContext<TUserPrimaryKey> : BaseDbContext<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public PostgreSqlDbContext(DbContextOptions<BaseDbContext<TUserPrimaryKey>> options)
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
			modelBuilder.Entity<Account<TUserPrimaryKey>>(account =>
			{
				account.ToTable("accessible_accounts");

				account.HasMany(sa => sa.AccessHistories)
					.WithOne(accessHistory => accessHistory.AccessibleAccount);
			});
		}

		private static void BuildAccessHistory(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<History<TUserPrimaryKey>>(history =>
			{
				history.ToTable("accessible_accounts_histories");

				history
					.HasOne(ah => ah.AccessibleAccount)
					.WithMany()
					.HasForeignKey(ah => ah.AccountId)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
