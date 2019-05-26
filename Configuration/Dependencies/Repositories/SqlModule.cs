namespace Configuration.Dependencies.Repositories
{
    using Autofac;

    using Infrastructure.DbContexts.Sql;
    using Infrastructure.Repositories;
    using Infrastructure.Repositories.Accounts;
    using Infrastructure.Repositories.Histories;

    internal class SqlModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SqlDbContext>().AsSelf();
			builder.RegisterType<AccountSqlRepository>().As<IAccountRepository>();
			builder.RegisterType<HistorySqlRepository>().As<IHistoryRepository>();
		}
	}
}
