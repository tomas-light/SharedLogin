namespace Core.Services.Accounts
{
    using AutoMapper;
    using System;
	using Domain = Core.Models;
	using Data = Infrastructure.Models;

	public class AccountMappingProfile<TUserPrimaryKey> : Profile
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public AccountMappingProfile()
		{
			MapDataModelToDomain();
			MapDomainModelToData();
		}

		private void MapDataModelToDomain()
		{
			CreateMap<Domain.Account<TUserPrimaryKey>, Data.Account<TUserPrimaryKey>>();
		}

		private void MapDomainModelToData()
		{
			CreateMap<Data.Account<TUserPrimaryKey>, Domain.Account<TUserPrimaryKey>>();
		}
	}
}
