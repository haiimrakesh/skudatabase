
using System.ComponentModel.DataAnnotations;


/// <summary>
/// Represents the configuration sequence for SKU.
/// </summary>
public class CreateSKUConfigSequenceRequest
{
    /// <summary>
    /// Gets or sets the identifier for the SKU part configuration.
    /// </summary>
    [Required(ErrorMessage = "SKU Part Configuration ID is required.")]
    public int SKUPartConfigId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the SKU configuration.
    /// </summary>
    [Required(ErrorMessage = "SKU Configuration ID is required.")]
    public int SKUConfigId { get; set; }

    /// <summary>
    /// Gets or sets the sequence number for the SKU configuration.
    /// </summary>
    [Required(ErrorMessage = "Sequence number is required.")]
    [Range(1, 25, ErrorMessage = "Sequence number must be between 1 and 25.")]
    public int Sequence { get; set; }

    /// <summary>
    /// Space to describe the relationship between the SKU configuration and the SKU part configuration.
    /// This could be used to provide additional context or information about the relationship.
    /// </summary>
    public string RelationshipDescription { get; set; } = string.Empty;
}