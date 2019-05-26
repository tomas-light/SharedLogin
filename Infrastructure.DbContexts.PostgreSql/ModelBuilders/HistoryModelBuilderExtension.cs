namespace Infrastructure.DbContexts.PostgreSql.ModelBuilders
{
	using Infrastructure.Entities;
	using Microsoft.EntityFrameworkCore;

	public static class HistoryModelBuilderExtension
	{
		public static ModelBuilder BuildHistories(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<History>(history =>
			{
				history.ToTable("accessible_accounts_histories");

				history
					.HasOne(ah => ah.AccessibleAccount)
					.WithMany()
					.HasForeignKey(ah => ah.AccountId)
					.OnDelete(DeleteBehavior.Restrict);
			});

			return modelBuilder;
		}
	}
}
