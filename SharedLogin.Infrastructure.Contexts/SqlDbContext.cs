namespace SharedLogin.Infrastructure.Contexts
{
	using Microsoft.EntityFrameworkCore;
    using SharedLogin.Domain;

    public class SqlDbContext<TAccountPrimaryKey> : BaseDbContext<TAccountPrimaryKey>
	{
		// Is it work ???
		// DbContextOptions<SqlDbContext<TAccountPrimaryKey>> options
		public SqlDbContext(DbContextOptions<BaseDbContext<TAccountPrimaryKey>> options)
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
			modelBuilder.Entity<SharedAccount<TAccountPrimaryKey>>(sharedAccount =>
			{
				sharedAccount.HasMany(ah => ah.AccessHistories)
					.WithOne(accessHistory => accessHistory.SharedAccount);
			});
		}

		private static void BuildAccessHistory(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<AccessHistory<TAccountPrimaryKey>>();

			//modelBuilder.Entity<AccessHistory<TAccountPrimaryKey>>(accessHistory =>
			//{
			//	accessHistory.HasKey(ah => new { ah.Id }).HasName("PK_access_history");
			//});
		}
	}
}
