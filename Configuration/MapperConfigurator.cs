namespace Configuration
{
    using AutoMapper;
    using Core.Services.Accounts;
    using Core.Services.Histories;

    internal class MapperConfigurator
	{
		public static IMapper Configure()
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile<AccountMappingProfile>();
				mc.AddProfile<HistoryMappingProfile>();
			});

			var mapper = mappingConfig.CreateMapper();
			return mapper;
		}
	}
}
