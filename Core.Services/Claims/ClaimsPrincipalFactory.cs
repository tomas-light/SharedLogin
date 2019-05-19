namespace Core.Services.Claims
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Options;
    using System;
    using System.Security.Claims;
	using System.Threading.Tasks;

	public class ClaimsPrincipalFactory<TUserPrimaryKey> : UserClaimsPrincipalFactory<IdentityUser<TUserPrimaryKey>>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public ClaimsPrincipalFactory(
			UserManager<IdentityUser<TUserPrimaryKey>> userManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, optionsAccessor)
		{
		}

		public override async Task<ClaimsPrincipal> CreateAsync(IdentityUser<TUserPrimaryKey> user)
		{
			var principal = await base.CreateAsync(user);

			var claimsIdentity = (ClaimsIdentity)principal.Identity;
			claimsIdentity.AddClaims(
				new[] {
					new Claim(ClaimTypes.ActiveAccountId, user.Id.ToString())
				});

			return principal;
		}
	}
}
