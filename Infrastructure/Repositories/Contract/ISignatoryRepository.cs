using Api.BizSign.Core.Models;

namespace Api.BizSign.Infrastructure.Repositories.Contract;

public interface ISignatoryRepository
{
    public Task<Signatory> CreateAsync(Signatory signatory);
}