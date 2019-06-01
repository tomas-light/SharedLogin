namespace WebApp.Data
{
	using Microsoft.AspNetCore.Identity;

	public class Role : IdentityRole<string>
	{
		public Role()
		{
		}

		public Role(string name) : base(name)
		{
		}
	}
}
