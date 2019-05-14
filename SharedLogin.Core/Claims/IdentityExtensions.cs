namespace SharedLogin.Core.Claims
{
    using System.Security.Claims;

	public static class IdentityExtensions
	{
		public static string GetAccountId(this ClaimsPrincipal identity)
		{
			Claim claim = identity?.FindFirst(LoginClaimTypes.AccountId);

			if (claim == null)
			{
				return string.Empty;
			}

			return claim.Value;
		}
	}
}
