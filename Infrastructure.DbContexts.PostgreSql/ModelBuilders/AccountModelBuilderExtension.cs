namespace Infrastructure.DbContexts.PostgreSql.ModelBuilders
{
    using Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;

	public static class AccountModelBuilderExtension
	{
		public static ModelBuilder BuildAccounts(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>(account =>
			{
				account.ToTable("accessible_accounts");

				account.HasMany(sa => sa.AccessHistories)
					.WithOne(accessHistory => accessHistory.AccessibleAccount);
			});

			return modelBuilder;
		}
	}
}
