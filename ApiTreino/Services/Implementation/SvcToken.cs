using ApiTreino.Services.Interfaces;
using ApiTreino.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiTreino.Services.Implementation
{
    public class SvcToken : ISvcToken
    {

        public string GerarToken(IdentityUser<int> user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GeraKey.GetKey());
            var claims = new Claim[]
            {
                    new Claim("username", user.UserName),
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role)
            };

            var tokenDecriptado = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDecriptado);
            return tokenHandler.WriteToken(token);
        }
    }
}