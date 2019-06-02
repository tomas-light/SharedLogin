namespace WebApp.Models.Account.Request
{
	using System.ComponentModel.DataAnnotations;

	public class ActivateAccountDTO
	{
		[Required]
		public string AccountId { get; set; }
	}
}
