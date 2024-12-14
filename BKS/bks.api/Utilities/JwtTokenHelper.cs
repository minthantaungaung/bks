using bks.domain.Data.Appsettings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bks.api.Utilities
{
	public class JwtTokenHelper
	{
		private readonly string _jwtSecret;
		private readonly string _issuer;
		private readonly string _audience;
		private readonly IDatabase _redisDb;

		public JwtTokenHelper(IOptions<JwtSettings> jwtSettings, IConnectionMultiplexer redis)
		{
			_jwtSecret = jwtSettings.Value.Secret;
			_issuer = jwtSettings.Value.Issuer;
			_audience = jwtSettings.Value.Audience;
			_redisDb = redis.GetDatabase();
		}

		public string GenerateToken(Guid userId, string email)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _issuer,
				audience: _audience,
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			// Store the token in Redis
			_redisDb.StringSet($"Token:{userId}", tokenString, TimeSpan.FromHours(1));

			return tokenString;
		}

		public bool ValidateToken(string token, Guid userId)
		{
			// Retrieve token from Redis
			var storedToken = _redisDb.StringGet($"Token:{userId}");
			return storedToken == token;
		}

		public void InvalidateToken(Guid userId)
		{
			_redisDb.KeyDelete($"Token:{userId}");
		}
	}

}
