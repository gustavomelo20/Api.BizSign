using Api.BizSign.Core.Models;

namespace Api.BizSign.Infrastructure.Repositories.Contract;

public interface IDocumentRepository
{
    public Task<Document?>  CreateAsync(Document user);
    public Task<List<Document>> GetByOwnerIdAsync(string ownerId);
}