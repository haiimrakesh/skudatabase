namespace SKUApp.Presentation.DataTransferObjects.ViewModels;
public class SKUConfigSequenceViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the SKU configuration sequence.
    /// </summary>
    public int SKUConfigSequenceId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the SKU part configuration.
    /// </summary>
    public int SKUPartConfigId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the SKU configuration.
    /// </summary>
    public int SKUConfigId { get; set; }

    /// <summary>
    /// Gets or sets the sequence number for the SKU configuration.
    /// </summary>
    public int Sequence { get; set; }

    /// <summary>
    /// Gets or sets the name of the SKU part configuration.
    /// </summary>
    public string SKUPartName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the length of the SKU part.
    /// </summary>
    public int SKUPartLength { get; set; }

    public string RelationshipDescription { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the SKUPart Configuration ViewModel. It can be null and only be 
    /// populated if full SKUConfig is requested.
    /// </summary>
    public SKUPartConfigViewModel? SKUPartConfig { get; set; }
}
