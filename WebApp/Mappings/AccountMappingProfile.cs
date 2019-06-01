namespace WebApp.Mappings
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using WebApp.Models.Account.Response;

    public class AccountMappingProfile : Profile
	{
		public AccountMappingProfile()
		{
			this.MapResponseModels();
		}

		private void MapResponseModels()
		{
			CreateMap<IdentityUser, AccountDTO>()
				.ForMember(model => model.Name, opt => opt.MapFrom(user => user.UserName));

			CreateMap<IdentityRole, AccountDTO>()
				.ForMember(model => model.Id, opt => opt.Ignore())
				.ForMember(model => model.Name, opt => opt.Ignore())
				.ForMember(model => model.RoleId, opt => opt.MapFrom(role => role.Id))
				.ForMember(model => model.RoleName, opt => opt.MapFrom(role => role.Name));
		}
	}
}
