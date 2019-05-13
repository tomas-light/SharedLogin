namespace SharedLogin.Infrastructure.Contexts
{
	using Microsoft.EntityFrameworkCore;
    using SharedLogin.Domain;

    public class SqlDbContext : BaseDbContext
	{
		// Is it work ???
		// DbContextOptions<SqlDbContext> options
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
				//accessHistory.HasKey(ah => new { ah.Id }).HasName("PK_access_history");

				accessHistory
					.HasOne(ah => ah.SharedAccount)
					.WithMany()
					.HasForeignKey(ah => ah.SharedAccountId)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
