namespace WebApp.Models.History.Response
{
	using System;

	public class AccessHistoryDTO
	{
		public string UserId { get; set; }

		public string LoginDateTime { get; set; }

		public string LogoutDateTime { get; set; }
	}
}
