using System.ComponentModel.DataAnnotations;
namespace SKUApp.Presentation.DataTransferObjects.RequestResponse;
/// <summary>
/// Represents a request to create a SKU configuration.
/// </summary>
public class UpdateSKUPartEntryRequest : CreateSKUPartEntryRequest
{
    /// <summary>
    /// Id of the SKU Part Entry being updated.
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }
}
