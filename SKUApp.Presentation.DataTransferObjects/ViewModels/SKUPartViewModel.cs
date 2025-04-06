namespace SKUApp.Presentation.DataTransferObjects.ViewModels;

/// <summary>
/// Represents the configuration for a SKU part.
/// </summary>
public class SKUPartConfigViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the SKU part configuration.
    /// </summary>
    public int SKUPartConfigId { get; set; }

    /// <summary>
    /// Gets or sets the name of the SKU part configuration.
    /// </summary>
    public string SKUPartConfigName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the length of the SKU part.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets the generic name of the SKU part.
    /// </summary>
    public string GenericName { get; set; } = "UNKNOWN";

    /// <summary>
    /// Gets or sets a value indicating whether the SKU part is alphanumeric.
    /// </summary>
    public bool IsAlphaNumeric { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the SKU part is case sensitive.
    /// </summary>
    public bool IsCaseSensitive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether preceding zeros are allowed in the SKU part.
    /// </summary>
    public bool AllowPreceedingZero { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether conflicting letters and characters are restricted in the SKU part.
    /// </summary>
    public bool RestrictConflictingLettersAndCharacters { get; set; }

    /// <summary>
    /// Indicates if a Hyphen is included at the end of the SKU Part entry. Increases the length by 1.
    /// </summary>
    public bool IncludeSpacerAtTheEnd { get; set; } = false;

    /// <summary>
    /// Gets or sets the width of the SKU configuration.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Description of the SKU Part.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of SKU part entries.
    /// </summary>
    public List<SKUPartEntryViewModel> SKUPartEntries { get; set; } = new List<SKUPartEntryViewModel>();
}