namespace WebApp.Models.Auth
{
    using System.ComponentModel.DataAnnotations;

	public class RegisterRoleDTO
	{
		[Required]
		public string Name { get; set; }
	}
}
