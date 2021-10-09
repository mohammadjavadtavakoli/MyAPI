using Entities;
using Microsoft.IdentityModel.Tokens;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class JwtService: IJwtService
    {

        public string Generate(User user)
        {
            var secretkey = Encoding.UTF8.GetBytes("MySecretKey123456789");
            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha256);
            var claim = _GetClaim(user);

            var descriotion = new SecurityTokenDescriptor
            {
                Issuer = "MyWebSite",
                Audience = "MyWebSite",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = SigningCredentials,
                Subject =new ClaimsIdentity(claim)

            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var securityToken = TokenHandler.CreateToken(descriotion);
            var jwt = TokenHandler.WriteToken(securityToken);

            return jwt;
        }

        private IEnumerable<Claim> _GetClaim(User user)
        {
            //JwtRegisteredClaimNames
            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim("PhoneNumber","09120746448")
            };

            var roles = new Role[] { new Role { Name = "admin" } };
            foreach (var item in roles)
            {
                list.Add(new Claim(ClaimTypes.Role, item.Name));
            }
            return list;
        }
    }
}
