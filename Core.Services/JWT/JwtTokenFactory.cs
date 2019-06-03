namespace Core.Services.JWT
{
    using Microsoft.IdentityModel.Tokens;
    using System;
	using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;

	public class JwtTokenFactory
	{
		private const string IssuerKey = "iss";
		private const string IssuedAtKey = "iat";
		private const string ExpiresKey = "exp";
		private const string NotBeforeKey = "nbf";
		private const string AudienceKey = "aud";

		public JwtSecurityToken Create(IEnumerable<Claim> claims, SharedAuthOptions options)
		{
			var notBefore = DateTime.Now;
			var expires = notBefore.Add(TimeSpan.FromMinutes(SharedAuthOptions.LifeTimeInMinutes));
			var securityKey = SharedAuthOptions.GenerateSymmetricSecurityKey();

			claims = (
						from claim in claims
						join type in claims.Select(c => c.Type) on claim.Type equals type
						where 
							type != IssuerKey && 
							type != IssuedAtKey && 
							type != ExpiresKey && 
							type != NotBeforeKey && 
							type != AudienceKey
						select claim)
					.ToList();

			var token = new JwtSecurityToken(
				SharedAuthOptions.Issuer,
				SharedAuthOptions.Audience,
				claims,
				notBefore,
				expires,
				new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
			);

			return token;
		}
	}
}
