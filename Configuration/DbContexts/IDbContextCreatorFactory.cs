namespace Configuration.DbContexts
{
	using Infrastructure.DbContexts;

	internal interface IDbContextCreatorFactory
	{
		IDbContextCreator Make(DbConfigurationOptions dbConfigurationOptions);
	}
}
