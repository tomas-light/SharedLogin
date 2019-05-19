namespace Infrastructure.Models
{
	using System;
    using System.Collections.Generic;

    public class Account
	{
		public int Id { get; set; }

		public string AccessibleAccountId { get; set; }

		public string OwnerId { get; set; }

		public virtual ICollection<History> AccessHistories { get; set; }
	}
}
