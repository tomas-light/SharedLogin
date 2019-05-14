namespace SharedLogin.Infrastructure.Contexts
{
	using Microsoft.EntityFrameworkCore;
    using SharedLogin.Core.DataModels;

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
			modelBuilder.Entity<SharedAccount>(sharedAccount =>
			{
				sharedAccount.ToTable("SharedAccounts");

				sharedAccount.HasMany(sa => sa.AccessHistories)
					.WithOne(accessHistory => accessHistory.SharedAccount);
			});
		}

		private static void BuildAccessHistory(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AccessHistory>(accessHistory =>
			{
				accessHistory.ToTable("AccessHistories");

				accessHistory
					.HasOne(ah => ah.SharedAccount)
					.WithMany()
					.HasForeignKey(ah => ah.SharedAccountId)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
