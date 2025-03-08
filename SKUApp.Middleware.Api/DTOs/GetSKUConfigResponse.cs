namespace SKUApp.Middleware.Api.DTOs
{
    public class GetSKUConfigResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Length { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}