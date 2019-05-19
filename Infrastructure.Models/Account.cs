namespace Infrastructure.Models
{
	using System;
    using System.Collections.Generic;

    public class Account<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public int Id { get; set; }

		public TUserPrimaryKey AccessibleAccountId { get; set; }

		public TUserPrimaryKey OwnerId { get; set; }

		public virtual ICollection<History<TUserPrimaryKey>> AccessHistories { get; set; }
	}
}
