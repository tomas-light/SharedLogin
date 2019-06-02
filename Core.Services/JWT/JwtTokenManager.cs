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

		public IEnumerable<Claim> GetClaimsFromEncodedJwtToken(string encodedJwtToken)
		{
			var jwtToken = DecodeJwt(encodedJwtToken);
			return jwtToken.Claims;
		}

		public JwtSecurityToken DecodeJwt(string encodedJwtToken)
		{
			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(encodedJwtToken);

			return jwtToken;
		}
	}
}
