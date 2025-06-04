using System.Text.Json.Serialization;

namespace Api.BizSign.WebApi.Request;

public class AuthRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}