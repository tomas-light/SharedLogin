namespace Configuration
{
    using Microsoft.EntityFrameworkCore;
	using Infrastructure.DbContexts;

	internal class DbContextConfigurator
	{
		public void Configure(BaseDbContext context)
		{
			context.Database.EnsureCreated();
			context.Database.Migrate();
		}
	}
}
