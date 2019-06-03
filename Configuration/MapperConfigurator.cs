namespace Configuration
{
    using AutoMapper;
    using Core.Services.Accounts;
    using Core.Services.Histories;
    using System;

    internal class MapperConfigurator
	{
		public static IMapper Configure(Action<IMapperConfigurationExpression> mapperExpression)
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mapperExpression(mc);
				mc.AddProfile<AccountMappingProfile>();
				mc.AddProfile<HistoryMappingProfile>();
			});

			var mapper = mappingConfig.CreateMapper();
			return mapper;
		}
	}
}
