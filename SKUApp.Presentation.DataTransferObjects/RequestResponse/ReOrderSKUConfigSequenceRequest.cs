using System.ComponentModel.DataAnnotations;
namespace SKUApp.Presentation.DataTransferObjects.RequestResponse;
/// <summary>
/// Represents a request to create a SKU configuration.
/// </summary>
public class ReOrderSKUConfigSequenceRequest
{
    /// <summary>
    /// Id of the SKUConfig being updated.
    /// </summary>
    [Key]
    [Required]
    public int SKUConfigId { get; set; }

    /// <summary>
    /// List of SKUConfigSequence Ids to be reordered.
    /// Must equal the number of SKUConfigSequence Ids in the SKUConfig.
    /// </summary>
    public List<int> SKUConfigSequenceIds { get; set; } = new List<int>();
}
