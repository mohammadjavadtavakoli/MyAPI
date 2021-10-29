using Common;
using Entities;
using Microsoft.Extensions.Options;
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
        private readonly SiteSettings siteSettings;
        public JwtService(IOptionsSnapshot<SiteSettings> optionsSnapshot)
        {
            this.siteSettings = optionsSnapshot.Value;
        }

        public string Generate(User user)
        {
            var secretkey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.SecretKey);
            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha256);

            var Encryptkey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.Encryptkey);
            var EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(Encryptkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            var claim = _GetClaim(user);

            var descriotion = new SecurityTokenDescriptor
            {
                Issuer = siteSettings.JwtSettings.Issuer,
                Audience = siteSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(siteSettings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddHours(siteSettings.JwtSettings.ExpirationMinutes),
                SigningCredentials = SigningCredentials,
                EncryptingCredentials= EncryptingCredentials,
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
                new Claim("PhoneNumber","09120746448"),
                new Claim("SecurityStamp",user.SecurityStamp.ToString())
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
