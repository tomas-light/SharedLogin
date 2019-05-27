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
			CreateMap<Domain.History, Data.History>();
			CreateMap<Data.History, Domain.History>();
		}
	}
}
