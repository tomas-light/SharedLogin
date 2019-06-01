namespace WebApp.Data
{
	using Microsoft.AspNetCore.Identity;

	public class User : IdentityUser<string>
	{
		public string Name { get; set; }

		public string Avatar { get; set; }
	}
}
