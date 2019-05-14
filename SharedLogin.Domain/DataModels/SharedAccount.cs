namespace SharedLogin.Core.DataModels
{
	using System.Collections.Generic;

	public class SharedAccount : DataModel
	{
		public int Id { get; set; }

		public string AccountId { get; set; }

		public string UserId { get; set; }

		public ICollection<AccessHistory> AccessHistories { get; set; }
	}
}
