using Microsoft.EntityFrameworkCore;
using Api.BizSign.Data;
using Api.BizSign.Models;
using Api.BizSign.Repositories.Contract;


namespace Api.BizSign.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly DatabaseContext _dbContext;

    public DocumentRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Document?> CreateAsync(Document document)
    {
        _dbContext.Documents.Add(document);
        await _dbContext.SaveChangesAsync();
        return document;
    }
    
    public async Task<List<Document>> GetByOwnerIdAsync(string ownerId)
    {
        return await _dbContext.Documents
            .Include(d => d.Signatories)
            .Where(d => d.OwnerID == ownerId)
            .ToListAsync();
    }
}