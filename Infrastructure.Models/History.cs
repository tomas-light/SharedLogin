namespace Infrastructure.Models
{
	using System;

	public class History<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public string AccessibleAccountName { get; set; }

		public string OwnerName { get; set; }

		public DateTime LoginDateTime { get; set; }

		public DateTime? LogoutDateTime { get; set; }

		public Account<TUserPrimaryKey> AccessibleAccount { get; set; }
	}
}
