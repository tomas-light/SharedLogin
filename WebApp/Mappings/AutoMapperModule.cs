namespace WebApp.Mappings
{
    using Autofac;
    using AutoMapper;

	public class AutoMapperModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile<AccountMappingProfile>();
			});

			var mapper = mappingConfig.CreateMapper();

			builder.RegisterInstance(mapper).As<IMapper>().InstancePerDependency();
		}
	}
}
