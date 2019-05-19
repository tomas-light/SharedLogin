namespace Core.Models
{
	using System;

	public class Account<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public int Id { get; set; }

		public TUserPrimaryKey OwnerId { get; set; }

		public TUserPrimaryKey AccessibleAccountId { get; set; }
	}
}
