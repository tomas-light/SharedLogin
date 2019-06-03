namespace WebApp.Models.Auth
{
    using System.ComponentModel.DataAnnotations;

	public class LoginDTO
	{
		[Required]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
