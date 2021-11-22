using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace serverApp.Helpers
{
    public class JwtService
    {
        private string securekey = "This is a very secure key";
        public string Generate(int Id)
        {
            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securekey));
            var credentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(Id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securekey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
    }
}
