namespace Core.Services.JWT
{
    using Microsoft.IdentityModel.Tokens;
	using System.Text;

	public class SharedAuthOptions
	{
		private static readonly string EncryptionKey = "U4MV%EFzQ8j5ok#tv4m2ntY28WJRZ9J5s!LoPiDLtLKa4ebaM^IJH1se%3XD";

		public static readonly string Issuer = "Tomas_Light";
		public static readonly string Audience = "SharedLogin.Client";

		// 12 hours
		public static readonly int LifeTimeInMinutes = 720;

		public static SymmetricSecurityKey GenerateSymmetricSecurityKey()
		{
			var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EncryptionKey));
			return securityKey;
		}
	}
}
