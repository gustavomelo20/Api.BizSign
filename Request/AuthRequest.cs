namespace Api.BizSign.Request;

public class AuthRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}