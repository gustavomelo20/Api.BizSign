using Api.BizSign.Models;

namespace Api.BizSign.Repositories.Contract;

public interface IUserRepository
{
    public Task<User?> GetByEmailAsync(string email);
    public Task<User>  CreateAsync(User user);
}
