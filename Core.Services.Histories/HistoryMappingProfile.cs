namespace Core.Services.Histories
{
	using AutoMapper;
	using System;
	using Domain = Core.Models;
	using Data = Infrastructure.Models;

	public class HistoryMappingProfile<TUserPrimaryKey> : Profile
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public HistoryMappingProfile()
		{
			MapDataModelToDomain();
			MapDomainModelToData();
		}

		private void MapDataModelToDomain()
		{
			CreateMap<Domain.History, Data.History<TUserPrimaryKey>>();
		}

		private void MapDomainModelToData()
		{
			CreateMap<Data.History<TUserPrimaryKey>, Domain.History>();
		}
	}
}
