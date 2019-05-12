namespace SharedLogin.Infrastructure.Contexts
{
	using Microsoft.EntityFrameworkCore;
    using SharedLogin.Domain;

    public class BaseDbContext<TAccountPrimaryKey> : DbContext
	{
		public BaseDbContext(DbContextOptions<BaseDbContext<TAccountPrimaryKey>> options)
			: base(options)
		{
		}

		public DbSet<SharedAccount<TAccountPrimaryKey>> SharedAccounts { get; set; }

		public DbSet<AccessHistory<TAccountPrimaryKey>> AccessHistories { get; set; }
	}
}
