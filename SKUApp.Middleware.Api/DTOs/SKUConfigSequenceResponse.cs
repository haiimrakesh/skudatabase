namespace SKUApp.Middleware.Api.DTOs;

public class SKUConfigSequenceResponse
{
    public int Id { get; set; }
    public int SKUConfigId { get; set; }
    public int Sequence { get; set; }
    public string SKUPartConfigName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}