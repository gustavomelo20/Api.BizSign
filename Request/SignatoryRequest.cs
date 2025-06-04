namespace Api.BizSign.Request;

public class SignatoryRequest
{
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty; 
    
    public string Phone { get; set; } = string.Empty; 
    
    public string Role { get; set; } = string.Empty; 
    
    public string Company { get; set; } = string.Empty; 
}