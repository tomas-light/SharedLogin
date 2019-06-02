namespace Configuration
{
    using Core.Services.JWT;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;

	public class JwtBearerConfigurator
	{
		public JwtBearerOptions CreateOptions(JwtBearerOptions options)
		{
			//options.RequireHttpsMetadata = false;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				// укзывает, будет ли валидироваться издатель при валидации токена
				ValidateIssuer = true,

				// строка, представляющая издателя
				ValidIssuer = SharedAuthOptions.Issuer,
 
				// будет ли валидироваться потребитель токена
				ValidateAudience = true,

				// установка потребителя токена
				ValidAudience = SharedAuthOptions.Audience,

				// будет ли валидироваться время существования
				//ValidateLifetime = true,

				// установка ключа безопасности
				IssuerSigningKey = SharedAuthOptions.GenerateSymmetricSecurityKey(),

				// валидация ключа безопасности
				ValidateIssuerSigningKey = true,
			};

			return options;
		}
	}
}
