using System.ComponentModel.DataAnnotations;
namespace SKUApp.Presentation.DataTransferObjects.RequestResponse;
/// <summary>
/// Represents a request to create a SKU configuration.
/// </summary>
public class UpdateSKUPartConfigRequest : CreateSKUPartConfigRequest
{
    /// <summary>
    /// Id of the SKU part config being updated.
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }
}
