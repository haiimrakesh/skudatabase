namespace SKUApp.Presentation.DataTransferObjects.ViewModels;

public class SKUConfigViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the SKU configuration.
    /// </summary>
    public int SKUConfigId { get; set; }

    /// <summary>
    /// Gets or sets the name of the SKU configuration.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the length of the SKU configuration.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets the width of the SKU configuration.
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Description of the SKU
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of SKU configuration sequences.
    /// </summary>
    public List<SKUConfigSequenceViewModel> SKUConfigSequenceViewModels { get; set; } = new List<SKUConfigSequenceViewModel>();
}
