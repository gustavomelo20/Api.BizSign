namespace Api.BizSign.WebApi.Request;

public class DocumentSaveRequest
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IFormFile File { get; set; }

    public List<SignatoryRequest> Signatories { get; set; } = new();
}