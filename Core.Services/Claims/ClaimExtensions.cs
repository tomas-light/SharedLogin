namespace Core.Services.Claims
{
    using System.Security.Claims;

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
	}
}
