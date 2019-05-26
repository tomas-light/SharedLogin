namespace Configuration.DbContexts
{
	using Infrastructure.DbContexts;
    using Infrastructure.DbContexts.PostgreSql;
    using Infrastructure.DbContexts.Sql;

	internal class DbContextCreatorFactory : IDbContextCreatorFactory
	{
		public IDbContextCreator Make(DbConfigurationOptions dbConfigurationOptions)
		{
			switch (dbConfigurationOptions)
			{
				case DbConfigurationOptions.PostgreSql:
					return new SqlContextCreator();

				case DbConfigurationOptions.Sql:
				default:
					return new PostgreSqlContextCreator();
			}
		}
	}
}
