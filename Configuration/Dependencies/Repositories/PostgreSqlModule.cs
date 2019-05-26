namespace Configuration.Dependencies.Repositories
{
    using Autofac;

    using Infrastructure.DbContexts.PostgreSql;
    using Infrastructure.Repositories;
    using Infrastructure.Repositories.Accounts;
    using Infrastructure.Repositories.Histories;

    internal class PostgreSqlModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<PostgreSqlDbContext>().AsSelf();
			builder.RegisterType<AccountPostgreSqlRepository>().As<IAccountRepository>();
			builder.RegisterType<HistoryPostgreSqlRepository>().As<IHistoryRepository>();
		}
	}
}
