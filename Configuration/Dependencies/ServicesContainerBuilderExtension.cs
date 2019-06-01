namespace Configuration.Dependencies
{
    using Autofac;

    using Core.Services;
    using Core.Services.Accounts;
    using Core.Services.Histories;
    using Microsoft.AspNetCore.Identity;
    using System;

    internal static class ServicesContainerBuilderExtension
	{
		public static ContainerBuilder RegisterServices<TUser, TRole, TKey>(this ContainerBuilder builder)
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TKey : IEquatable<TKey>
		{
			builder.RegisterType<AccountService<TUser, TRole, TKey>>().As<IAccountService<TUser, TRole, TKey>>();
			builder.RegisterType<HistoryService>().As<IHistoryService>();

			return builder;
		}
	}
}
