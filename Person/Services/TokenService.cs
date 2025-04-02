﻿using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PersonTransation.Models;

namespace Person.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UsersModel person)
        {
            var key = _configuration["Key:Jwt"];
            var tokenConfig = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(tokenConfig, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("PersonId", person.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = credentials,
                Issuer = _configuration["Key:Jwt"]
            };

            var tokenhandler = new JsonWebTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);
            

            return $"Token: {token} Id: {person.Id}";
        }
    }
}
