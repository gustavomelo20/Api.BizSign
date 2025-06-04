using Api.BizSign.Models;

namespace Api.BizSign.Services.Contracts;

public interface IAuthService
{
    public Task<User?> Register(User user);
    public Task<string> LoginAsync(User login);
}