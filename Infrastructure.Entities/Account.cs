namespace Infrastructure.Entities
{
	using System.Collections.Generic;

	public class Account
	{
		public int Id { get; set; }

		public string UserId { get; set; }

		public string AccessibleAccountId { get; set; }

		public virtual ICollection<History> AccessHistories { get; set; }
	}
}
