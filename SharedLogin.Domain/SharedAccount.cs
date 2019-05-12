namespace SharedLogin.Domain
{
	using System.Collections.Generic;

	public class SharedAccount<TAccountPrimaryKey> : DomainModel
	{
		public int Id { get; set; }

		public TAccountPrimaryKey AccountId { get; set; }

		public TAccountPrimaryKey UserId { get; set; }

		public ICollection<AccessHistory<TAccountPrimaryKey>> AccessHistories { get; set; }
	}
}
