namespace Configuration
{
    using AutoMapper;
    using Core.Services.Accounts;
    using Core.Services.Histories;
    using System;

    abstract class MapperConfiguration
	{
		public static IMapper Configure()
		{
			var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
			{
				mc.AddProfile<AccountMappingProfile>();
				mc.AddProfile<HistoryMappingProfile>();
			});

			var mapper = mappingConfig.CreateMapper();
			return mapper;
		}
	}
}
