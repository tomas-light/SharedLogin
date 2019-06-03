namespace WebApp.AppConfiguration
{
    using Autofac;
    using WebApp.AppConfiguration.Dependencies;

    public class Configurator
	{
		public ContainerBuilder ConfigureDependencies()
		{
			return IdentityConfigurator.Configure();
		}
	}
}
