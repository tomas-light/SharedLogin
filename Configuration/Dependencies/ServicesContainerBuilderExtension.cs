namespace Configuration.Dependencies
{
    using Autofac;

    using Core.Services;
    using Core.Services.Accounts;
    using Core.Services.Histories;

	internal static class ServicesContainerBuilderExtension
	{
		public static ContainerBuilder RegisterServices(this ContainerBuilder builder)
		{
			builder.RegisterType<AccountService>().As<IAccountService>();
			builder.RegisterType<HistoryService>().As<IHistoryService>();

			return builder;
		}
	}
}
