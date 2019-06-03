namespace WebApp.Models.Auth
{
    using System.ComponentModel.DataAnnotations;

	public class RegisterUserDTO
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Avatar { get; set; }

		[Required]
		public string RoleId { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
