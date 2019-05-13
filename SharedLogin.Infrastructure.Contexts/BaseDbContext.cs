namespace SharedLogin.Infrastructure.Contexts
{
	using Microsoft.EntityFrameworkCore;
    using SharedLogin.Domain;

    public class BaseDbContext : DbContext
	{
		public BaseDbContext(DbContextOptions<BaseDbContext> options)
			: base(options)
		{
		}

		public DbSet<SharedAccount> SharedAccounts { get; set; }

		public DbSet<AccessHistory> AccessHistories { get; set; }
	}
}
