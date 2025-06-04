using Api.BizSign.Core.Models;
using Api.BizSign.Core.Services.Contracts;
using Api.BizSign.Exceptions;
using Api.BizSign.Infrastructure.Repositories.Contract;

namespace Api.BizSign.Core.Services;

public class AuthService(IUserRepository repo, IConfiguration config, IJwtService jwtService) : IAuthService
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

        return jwtService.GenerateToken(user.Email, user.Id);
    }
}