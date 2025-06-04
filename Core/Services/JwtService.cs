using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.BizSign.Core.Services.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace Api.BizSign.Core.Services;

public class JwtService(IConfiguration config) : IJwtService
{
    public string GenerateToken(string email, Guid userId)
    {
        var key = Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}