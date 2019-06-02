namespace Core.Services.JWT
{
	using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

	public class JwtTokenManager
	{
		private readonly JwtTokenFactory jwtTokenFactory;

		public JwtTokenManager()
		{
			this.jwtTokenFactory = new JwtTokenFactory();
		}

		public JwtTokenManager(JwtTokenFactory jwtTokenFactory)
		{
			this.jwtTokenFactory = jwtTokenFactory;
		}

		public string EncodeJwt(IEnumerable<Claim> claims)
		{
			return EncodeJwt(claims, new SharedAuthOptions());
		}

		public string EncodeJwt(IEnumerable<Claim> claims, SharedAuthOptions options)
		{
			var token = this.jwtTokenFactory.Create(claims, options);
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
			return encodedJwt;
		}
	}
}
