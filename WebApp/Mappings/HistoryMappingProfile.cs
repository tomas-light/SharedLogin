namespace WebApp.Mappings
{
    using AutoMapper;
    using Core.Models;
    using WebApp.Models.History.Response;

    public class HistoryMappingProfile : Profile
	{
		public HistoryMappingProfile()
		{
			this.MapResponseModels();
		}

		private void MapResponseModels()
		{
			CreateMap<History, AccessHistoryDTO>()
				.ForMember(model => model.UserId, opt => opt.Ignore())
				.ForMember(model => model.LoginDateTime, opt => opt.MapFrom(history => history.LoginDateTime))
				.ForMember(model => model.LogoutDateTime, opt => opt.MapFrom(history => history.LogoutDateTime));

			CreateMap<Account, AccessHistoryDTO>()
				.ForMember(model => model.UserId, opt => opt.MapFrom(account => account.UserId));
		}
	}
}
