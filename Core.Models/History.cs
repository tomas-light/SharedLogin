namespace Core.Models
{
	using System;

	public class History
	{
		public int Id { get; set; }

		public string AccessibleAccountName { get; set; }

		public string UserName { get; set; }

		public DateTime LoginDateTime { get; set; }

		public DateTime? LogoutDateTime { get; set; }
	}
}
