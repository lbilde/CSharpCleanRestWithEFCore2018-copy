using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CustomerApp.Infrastructure.Data.Managers
{
    public class TokenManager
    {
        private string jwtKey;
        private double jwtExpireDays;
        private string jwtIssuer;

        public TokenManager(string jwtKey, double jwtExpireDays, string jwtIssuer)
        {
            this.jwtKey = jwtKey;
            this.jwtExpireDays = jwtExpireDays;
            this.jwtIssuer = jwtIssuer;
        }
        
        public async Task<string> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(jwtExpireDays);

            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}