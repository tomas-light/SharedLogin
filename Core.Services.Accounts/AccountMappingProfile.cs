namespace Core.Services.Accounts
{
    using AutoMapper;
	using Domain = Core.Models;
	using Data = Infrastructure.Entities;

	public class AccountMappingProfile : Profile
	{
		public AccountMappingProfile()
		{
			CreateMap<Domain.Account, Data.Account>();
			CreateMap<Data.Account, Domain.Account>();
		}
	}
}
