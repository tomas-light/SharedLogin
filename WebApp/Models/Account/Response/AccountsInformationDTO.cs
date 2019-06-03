namespace WebApp.Models.Account.Response
{
	public class AccountsInformationDTO
	{
		public AccountDTO AuthenticatedAccount { get; set; }

		public AccountDTO ActiveAccount { get; set; }

		public AccountDTO[] AccessibleAccounts { get; set; }
	}
}
