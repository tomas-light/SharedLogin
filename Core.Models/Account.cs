namespace Core.Models
{
	using System;

	public class Account
	{
		public int Id { get; set; }

		public string OwnerId { get; set; }

		public string AccessibleAccountId { get; set; }
	}
}
