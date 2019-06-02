namespace Core.Services.JWT
{
    using Microsoft.IdentityModel.Tokens;
    using System;
	using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

	public class JwtTokenFactory
	{
		public JwtSecurityToken Create(IEnumerable<Claim> claims, SharedAuthOptions options)
		{
			var notBefore = DateTime.Now;
			var expires = notBefore.Add(TimeSpan.FromMinutes(options.LifeTimeInMinutes));
			var securityKey = SharedAuthOptions.GenerateSymmetricSecurityKey();

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
