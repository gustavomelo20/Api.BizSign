namespace Api.BizSign.Core.Services.Contracts;

public interface IJwtService
{
    string GenerateToken(string email, Guid userId);
}