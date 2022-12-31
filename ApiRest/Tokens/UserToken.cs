﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiRest.Tokens;

public class UserToken : IUserToken
{
    private readonly IConfiguration _configuration;

    public UserToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateToken(UserModel user)
    {
        List<Claim> claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
