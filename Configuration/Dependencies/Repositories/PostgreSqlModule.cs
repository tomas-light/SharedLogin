namespace Configuration.Dependencies.Repositories
{
    using Autofac;
    using System;
	using Microsoft.EntityFrameworkCore;

	using Infrastructure.DbContexts;
	using Infrastructure.DbContexts.PostgreSql;
	using Infrastructure.Repositories;
	using Infrastructure.Repositories.Accounts;
	using Infrastructure.Repositories.Histories;

    internal class PostgreSqlModule : Module
	{
		private readonly IDbConfiguration dbConfiguration;

		public PostgreSqlModule(IDbConfiguration dbConfiguration)
		{
			this.dbConfiguration = dbConfiguration ?? throw new ArgumentException(nameof(dbConfiguration));
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<PostgreSqlDbContext>().AsSelf();
			builder.RegisterType<AccountPostgreSqlRepository>().As<IAccountRepository>();
			builder.RegisterType<HistoryPostgreSqlRepository>().As<IHistoryRepository>();

			builder.RegisterInstance(this.dbConfiguration).As<IDbConfiguration>().SingleInstance();
			builder.Register(context => PostgreSqlDbContextOptionsFactory.Create(dbConfiguration)).As<DbContextOptions<BaseDbContext>>();
		}
	}
}
