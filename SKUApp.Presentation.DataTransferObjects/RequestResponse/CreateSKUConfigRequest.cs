using System.ComponentModel.DataAnnotations;
namespace SKUApp.Presentation.DataTransferObjects.RequestResponse;
/// <summary>
/// Represents a request to create a SKU configuration.
/// </summary>
public class CreateSKUConfigRequest
{
    /// <summary>
    /// Gets or sets the name of the SKU configuration.
    /// </summary>
    /// <remarks>
    /// The name must be between 3 and 25 characters.
    /// </remarks>
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the SKU configuration.
    /// </summary>
    /// <remarks>
    /// The description must be between 3 and 100 characters.
    /// </remarks>
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 100 characters.")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the length of the SKU configuration.
    /// </summary>
    /// <remarks>
    /// The length must be between 3 and 25.
    /// </remarks>
    [Range(3, 25, ErrorMessage = "Length must be between 3 and 25.")]
    public int Length { get; set; }
}