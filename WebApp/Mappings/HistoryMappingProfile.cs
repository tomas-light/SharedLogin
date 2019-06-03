namespace WebApp.Mappings
{
    using AutoMapper;
    using Core.Models;
    using System;
    using System.Globalization;
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
				.ForMember(model => model.LoginDateTime, opt => opt.MapFrom(history => DateToString(history.LoginDateTime)))
				.ForMember(model => model.LogoutDateTime, opt => opt.MapFrom(history => DateToString(history.LogoutDateTime)));

			CreateMap<Account, AccessHistoryDTO>()
				.ForMember(model => model.UserId, opt => opt.MapFrom(account => account.UserId));
		}

		private string DateToString(DateTime? dateTime)
		{
			if (dateTime == null)
			{
				return string.Empty;
			}

			var format = "dd/MM/yyyy HH:mm:ss";
			var friendlyDate = ((DateTime)dateTime).ToString(format, CultureInfo.InvariantCulture);

			return friendlyDate;
		}
	}
}
