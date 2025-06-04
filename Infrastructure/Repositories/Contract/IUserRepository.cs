using Api.BizSign.Core.Models;

namespace Api.BizSign.Infrastructure.Repositories.Contract;

public interface IUserRepository
{
    public Task<User?> GetByEmailAsync(string email);
    public Task<User> CreateAsync(User user);
}