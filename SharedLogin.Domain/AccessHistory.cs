namespace SharedLogin.Domain
{
	using System;

	public class AccessHistory<TAccountPrimaryKey> : DomainModel
	{
		public int Id { get; set; }

		public TAccountPrimaryKey SharedAccountId { get; set; }

		public string AccountName { get; set; }

		public string UserName { get; set; }

		public DateTime LoginDateTime { get; set; }

		public DateTime? EndLoginDateTime { get; set; }

		public SharedAccount<TAccountPrimaryKey> SharedAccount { get; set; }
	}
}
