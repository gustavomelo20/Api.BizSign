using Api.BizSign.Models;

namespace Api.BizSign.Repositories.Contract;

public interface ISignatoryRepository
{
    public Task<Signatory>  CreateAsync(Signatory signatory);
}
