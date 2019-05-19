namespace Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
	using System.Collections.Generic;
	using System.Text;

	public static class Configger<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public static void Configure(IServiceCollection services, IConfiguration configuration, string dbConnectionString)
		{
			MapperConfiguration<TUserPrimaryKey>.Configure();
			DependencyConfiguration<TUserPrimaryKey>.Configure(services);
			DbContextConfiguration<TUserPrimaryKey>.Configure(services, dbConnectionString);
		}
	}
}
