namespace Configuration.Dependencies.Repositories
{
    using Autofac;
    using System;
	using Microsoft.EntityFrameworkCore;

	using Infrastructure.DbContexts;
	using Infrastructure.DbContexts.Sql;
	using Infrastructure.Repositories;
	using Infrastructure.Repositories.Accounts;
	using Infrastructure.Repositories.Histories;

    internal class SqlModule : Module
	{
		private readonly IDbConfiguration dbConfiguration;

		public SqlModule(IDbConfiguration dbConfiguration)
		{
			this.dbConfiguration = dbConfiguration ?? throw new ArgumentException(nameof(dbConfiguration));
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SqlDbContext>().AsSelf();
			builder.RegisterType<AccountSqlRepository>().As<IAccountRepository>();
			builder.RegisterType<HistorySqlRepository>().As<IHistoryRepository>();

			builder.RegisterInstance(this.dbConfiguration).As<IDbConfiguration>().SingleInstance();
			builder.Register(context => SqlDbContextOptionsFactory.Create(dbConfiguration)).As<DbContextOptions<BaseDbContext>>();
		}
	}
}
