namespace SharedLogin.Domain
{
	using System;

	public class AccessHistory : DomainModel
	{
		public int Id { get; set; }

		public int SharedAccountId { get; set; }

		public string AccountName { get; set; }

		public string UserName { get; set; }

		public DateTime LoginDateTime { get; set; }

		public DateTime? EndLoginDateTime { get; set; }

		public SharedAccount SharedAccount { get; set; }
	}
}
