namespace Configuration
{
    using Core.Services.Accounts;
    using Core.Services.Histories;
    using System;

    abstract class MapperConfiguration<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public static void Configure()
		{
			var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
			{
				mc.AddProfile<AccountMappingProfile<TUserPrimaryKey>>();
				mc.AddProfile<HistoryMappingProfile<TUserPrimaryKey>>();
			});

			var mapper = mappingConfig.CreateMapper();
		}
	}
}
