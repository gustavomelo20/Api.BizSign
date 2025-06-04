using Microsoft.EntityFrameworkCore;
using Api.BizSign.Data;
using Api.BizSign.Models;
using Api.BizSign.Repositories.Contract;


namespace Api.BizSign.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _dbContext;

    public UserRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
    }
    
    public async Task<User?> CreateAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}