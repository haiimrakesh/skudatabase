using System.ComponentModel.DataAnnotations;
namespace SKUApp.Presentation.DataTransferObjects.RequestResponse;
/// <summary>
/// Represents a request to create a SKU configuration.
/// </summary>
public class UpdateSKUPartEntryRequest
{
    /// <summary>
    /// Id of the SKU Part Entry being updated.
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the SKU part value.
    /// </summary>
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters.")]
    public string Name { get; set; } = null!;
}
