namespace WebApp.Models.History.Response
{
	using System;

	public class AccessHistoryDTO
	{
		public string UserId { get; set; }

		public DateTime LoginDateTime { get; set; }

		public DateTime? LogoutDateTime { get; set; }
	}
}
