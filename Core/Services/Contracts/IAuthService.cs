using Api.BizSign.Core.Models;

namespace Api.BizSign.Core.Services.Contracts;

public interface IAuthService
{
    public Task<User?> Register(User user);
    public Task<string> LoginAsync(User login);
}