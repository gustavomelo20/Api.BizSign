using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.BizSign.Core.Models;
using Api.BizSign.Core.Services.Contracts;
using Api.BizSign.Exceptions;
using Api.BizSign.Infrastructure.Repositories.Contract;
using Microsoft.IdentityModel.Tokens;

namespace Api.BizSign.Core.Services;

public class AuthService(IUserRepository repo, IConfiguration config) : IAuthService
{
    public async Task<User?> Register(User user)
    {
        var existingUser = await repo.GetByEmailAsync(user.Email);
        
        if (existingUser != null)
            throw new EmailInUseException();

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        user.Id = Guid.NewGuid();
        
        return await repo.CreateAsync(user);
    }
    
    public async Task<string> LoginAsync(User login)
    {
        var user = await repo.GetByEmailAsync(login.Email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash))
            throw new InvalidCredentialsException();

        return GenerateJwt(user.Email, user.Id);
    }
    
    private string GenerateJwt(string email, Guid userId)
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