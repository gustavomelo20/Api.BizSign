using Microsoft.EntityFrameworkCore;
using Api.BizSign.Data;
using Api.BizSign.Models;
using Api.BizSign.Repositories.Contract;


namespace Api.BizSign.Repositories;

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