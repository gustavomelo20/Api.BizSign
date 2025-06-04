using Api.BizSign.Core.Models;
using Microsoft.EntityFrameworkCore;
using Api.BizSign.Infrastructure.Data;
using Api.BizSign.Infrastructure.Repositories.Contract;


namespace Api.BizSign.Infrastructure.Repositories;

public class SignatoryRepository : ISignatoryRepository
{
    private readonly DatabaseContext _dbContext;

    public SignatoryRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Signatory> CreateAsync(Signatory signatory)
    {
        _dbContext.Signatories.Add(signatory);
        await _dbContext.SaveChangesAsync();
        return signatory;
    }
}