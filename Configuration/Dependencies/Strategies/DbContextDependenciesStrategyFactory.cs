namespace Configuration.Dependencies.Strategies
{
	using System;

	internal class DbContextDependenciesStrategyFactory
	{
		public IDbContextDependenciesStrategy Make(DbConfigurationOptions dbConfigurationOptions)
		{
			switch (dbConfigurationOptions)
			{
				case DbConfigurationOptions.PostgreSql:
					return new PostgreSqlRepositoryDependenciesStrategy();

				case DbConfigurationOptions.Sql:
					return new SqlRepositoryDependenciesStrategy();

				default:
					throw new ArgumentException(nameof(dbConfigurationOptions));
			}
		}
	}
}
