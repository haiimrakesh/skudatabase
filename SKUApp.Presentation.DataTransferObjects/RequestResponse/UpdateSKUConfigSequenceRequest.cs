
using System.ComponentModel.DataAnnotations;


/// <summary>
/// Represents the configuration sequence for SKU.
/// </summary>
public class UpdateSKUConfigSequenceRequest
{
    /// <summary>
    /// Gets or sets the identifier for the SKU part configuration.
    /// </summary>
    [Required(ErrorMessage = "SKU Config Sequence ID is required.")]
    public int SKUConfigSequenceId { get; set; }

    /// <summary>
    /// Space to describe the relationship between the SKU configuration and the SKU part configuration.
    /// This could be used to provide additional context or information about the relationship.
    /// </summary>
    public string RelationshipDescription { get; set; } = string.Empty;
}