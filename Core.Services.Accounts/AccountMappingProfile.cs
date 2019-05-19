namespace Core.Services.Accounts
{
    using AutoMapper;
    using System;
	using Domain = Core.Models;
	using Data = Infrastructure.Entities;

	public class AccountMappingProfile : Profile
	{
		public AccountMappingProfile()
		{
			MapDataModelToDomain();
			MapDomainModelToData();
		}

		private void MapDataModelToDomain()
		{
			CreateMap<Domain.Account, Data.Account>();
		}

		private void MapDomainModelToData()
		{
			CreateMap<Data.Account, Domain.Account>();
		}
	}
}
