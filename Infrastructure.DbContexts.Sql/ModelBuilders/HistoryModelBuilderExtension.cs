namespace Infrastructure.DbContexts.Sql.ModelBuilders
{
	using Infrastructure.Entities;
	using Microsoft.EntityFrameworkCore;

	public static class HistoryModelBuilderExtension
	{
		public static ModelBuilder BuildHistories(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<History>(history =>
			{
				history.ToTable("AccessibleAccountsHistories");

				history
					.HasOne(ah => ah.AccessibleAccount)
					.WithMany()
					.HasForeignKey(ah => ah.AccountId)
					.OnDelete(DeleteBehavior.SetNull);
			});

			return modelBuilder;
		}
	}
}
