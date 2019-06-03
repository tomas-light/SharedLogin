namespace Core.Services.Claims
{
	using System.Security.Claims;
	using IdentityClaimTypes = System.Security.Claims.ClaimTypes;

	public static class ClaimExtensions
	{
		public static string GetActiveAccountId(this ClaimsPrincipal identity)
		{
			Claim claim = identity?.FindFirst(ClaimTypes.ActiveAccountId);

			if (claim == null)
			{
				return string.Empty;
			}

			return claim.Value;
		}

		public static string GetActiveAccountRoleId(this ClaimsPrincipal identity)
		{
			Claim claim = identity?.FindFirst(ClaimTypes.ActiveAccountRoleId);

			if (claim == null)
			{
				return string.Empty;
			}

			return claim.Value;
		}

		public static string GetAuthenticatedAccountRoleName(this ClaimsPrincipal identity)
		{
			Claim claim = identity?.FindFirst(IdentityClaimTypes.Role);

			if (claim == null)
			{
				return string.Empty;
			}

			return claim.Value;
		}
	}
}
