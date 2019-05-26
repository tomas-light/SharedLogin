namespace Configuration.Dependencies
{
    using Autofac;

    using Infrastructure.Repositories;
    using Infrastructure.Repositories.Accounts;
    using Infrastructure.Repositories.Histories;

    internal static class RepositoriesContainerBuilderExtension
	{
		public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder)
		{
			builder
				.RegisterPostgreSqlRepository()
				.RegisterSqlRepository();

			return builder;
		}

		private static ContainerBuilder RegisterPostgreSqlRepository(this ContainerBuilder builder)
		{
			builder.RegisterType<AccountPostgreSqlRepository>().As<IAccountRepository>();
			builder.RegisterType<HistoryPostgreSqlRepository>().As<IHistoryRepository>();

			return builder;
		}

		private static ContainerBuilder RegisterSqlRepository(this ContainerBuilder builder)
		{
			builder.RegisterType<AccountSqlRepository>().As<IAccountRepository>();
			builder.RegisterType<HistorySqlRepository>().As<IHistoryRepository>();

			return builder;
		}
	}
}
