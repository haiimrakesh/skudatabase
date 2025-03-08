using System.ComponentModel.DataAnnotations;

namespace SKUApp.Middleware.Api.DTOs
{
    public class CreateSKUConfigRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 100 characters.")]
        public string Description { get; set; } = string.Empty;
        [Range(3, 25, ErrorMessage = "Length must be between 3 and 25.")]
        public int Length { get; set; }
    }
}