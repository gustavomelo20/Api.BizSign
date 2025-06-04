using Api.BizSign.Models;

namespace Api.BizSign.Repositories.Contract;

public interface IDocumentRepository
{
    public Task<Document?>  CreateAsync(Document user);

    public Task<List<Document>> GetByOwnerIdAsync(string ownerId);
}