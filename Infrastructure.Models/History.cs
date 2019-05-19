namespace Infrastructure.Models
{
	using System;

	public class History
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public string AccessibleAccountName { get; set; }

		public string OwnerName { get; set; }

		public DateTime LoginDateTime { get; set; }

		public DateTime? LogoutDateTime { get; set; }

		public Account AccessibleAccount { get; set; }
	}
}
