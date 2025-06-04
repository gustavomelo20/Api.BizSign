namespace Api.BizSign.WebApi.Request;

public class AuthRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}