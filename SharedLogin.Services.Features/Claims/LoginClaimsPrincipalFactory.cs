namespace SharedLogin.Services.Core.Claims
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Options;
	using SharedLogin.Core.Claims;
	using System.Security.Claims;
	using System.Threading.Tasks;

	public class LoginClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
	{
		public LoginClaimsPrincipalFactory(
			UserManager<IdentityUser> userManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, optionsAccessor)
		{
		}

		public override async Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
		{
			var principal = await base.CreateAsync(user);

			var claimsIdentity = (ClaimsIdentity)principal.Identity;
			claimsIdentity.AddClaims(
				new[] {
					new Claim(LoginClaimTypes.AccountId, user.Id)
				});

			return principal;
		}
	}
}
