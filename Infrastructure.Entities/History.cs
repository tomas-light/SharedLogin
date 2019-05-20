namespace Infrastructure.Entities
{
	using System;

	public class History
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public string UserName { get; set; }

		public string AccessibleAccountName { get; set; }

		public DateTime LoginDateTime { get; set; }

		public DateTime? LogoutDateTime { get; set; }

		public Account AccessibleAccount { get; set; }
	}
}
