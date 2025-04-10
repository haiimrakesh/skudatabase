using System.ComponentModel.DataAnnotations;
namespace SKUApp.Presentation.DataTransferObjects.RequestResponse;
/// <summary>
/// Represents a request to create a SKU configuration.
/// </summary>
public class UpdateSKUConfigRequest : CreateSKUConfigRequest
{
    /// <summary>
    /// Id of the SKUConfig being updated.
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }
}
