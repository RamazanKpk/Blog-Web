using Buisenss.Interface.Abstract.AuthAbstract;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Buisenss.Interface.AuthService
{
    public class AuthService : ITokenService
    {
        private static readonly string SecretKey = GenerateSecretKey();

        public AuthService()
        {
        }
         
        public string GenerateToken(string userName, bool isAdmin, int expireMinutes = 30)
        {
            var symmetricKey = Encoding.ASCII.GetBytes(SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
                }),
                Expires = now.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }
        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            try
            {
                var tokenHandleer = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandleer.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null)
                {
                    return null;
                }
                var symmetricKey = Encoding.ASCII.GetBytes(SecretKey);
                var vlaidationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                };
                SecurityToken securityToken;
                var principal = tokenHandleer.ValidateToken(token, vlaidationParameters, out securityToken);
                return principal;
            }

            catch (Exception)
            {

                return null;
            }
        }
        public static string GenerateSecretKey(int size= 32)
        {
            using (var serverProvider  = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[size];
                serverProvider.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
