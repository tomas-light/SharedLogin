namespace Infrastructure.DbContexts.Sql.ModelBuilders
{
    using Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;

	public static class AccountModelBuilderExtension
	{
		public static ModelBuilder BuildAccounts(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>(account =>
			{
				account.ToTable("AccessibleAccounts");

				account.HasMany(sa => sa.AccessHistories)
					.WithOne(accessHistory => accessHistory.AccessibleAccount);
			});

			return modelBuilder;
		}
	}
}
