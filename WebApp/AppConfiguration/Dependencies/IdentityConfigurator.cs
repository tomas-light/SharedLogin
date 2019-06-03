namespace WebApp.AppConfiguration.Dependencies
{
    using Autofac;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using WebApp.Data;

    public static class IdentityConfigurator
	{
		public static ContainerBuilder Configure()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<UserManager<User>>().AsSelf().InstancePerDependency();
			builder.RegisterType<RoleManager<Role>>().AsSelf().InstancePerDependency();

			builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
			builder.RegisterType<UserClaimsPrincipalFactory<User>>().As<IUserClaimsPrincipalFactory<User>>().InstancePerDependency();
			builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>().InstancePerDependency();
			builder.RegisterType<Logger<SignInManager<User>>>().As<ILogger<SignInManager<User>>>().InstancePerDependency();
			builder.RegisterType<AuthenticationSchemeProvider>().As<IAuthenticationSchemeProvider>().InstancePerDependency();

			builder.RegisterType<DbContextInitializer>().AsSelf().InstancePerDependency();

			builder.RegisterType<SignInManager<User>>().AsSelf().InstancePerDependency();

			return builder;
		}
	}
}
