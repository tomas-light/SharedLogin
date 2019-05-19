namespace Core.Services.Histories
{
	using AutoMapper;
	using System;
	using Domain = Core.Models;
	using Data = Infrastructure.Entities;

	public class HistoryMappingProfile : Profile
	{
		public HistoryMappingProfile()
		{
			MapDataModelToDomain();
			MapDomainModelToData();
		}

		private void MapDataModelToDomain()
		{
			CreateMap<Domain.History, Data.History>();
		}

		private void MapDomainModelToData()
		{
			CreateMap<Data.History, Domain.History>();
		}
	}
}
