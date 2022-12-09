using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using University_Backend.Models.JWT;

namespace University_Backend.Helpers
{
    public static class JwtHelper
    {
        public static UserToken GenerateToken(UserToken userToken, JWTsettings jwtSettings)
        {
            try
            {
                if (userToken == null)
                    throw new ArgumentNullException(nameof(userToken));

                // Obtain SECRET KEY
                byte[] key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id;

                // Expires in 1 Day
                DateTime expiredTime = DateTime.UtcNow.AddDays(1);

                // Validity of our token
                userToken.Validity = expiredTime.TimeOfDay;

                // Generate our JWT
                var jwt = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience, 
                    claims: GetClaims(userToken, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expiredTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256
                    )
                );

                return new UserToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                    Username = userToken.Username,
                    Id = userToken.Id,
                    GuidId = Id
                };
            }
            catch (System.Exception e)
            {
                throw new Exception($"Error generating the JWT: {e.Message}");
            }
        }

        public static IEnumerable<Claim> GetClaims(this UserToken userToken, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userToken, Id);
        }

        public static IEnumerable<Claim> GetClaims(this UserToken userToken, Guid Id)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", userToken.Id.ToString()),
                new Claim(ClaimTypes.Name, userToken.Username),
                new Claim(ClaimTypes.Email, userToken.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MM ddd dd yyyy HH:mm:ss tt"))
            };

            if (userToken.Username.Equals("Admin"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User1"));
            }

            return claims;
        }
    }
}