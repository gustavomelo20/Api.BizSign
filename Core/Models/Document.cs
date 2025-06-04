namespace Api.BizSign.Core.Models;

public class Document
{
    public Guid Id { get; set; }
    public string? OwnerID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<Signatory> Signatories { get; set; }
    public string File { get; set; } = string.Empty;
    public int Progress { get; set; }
}