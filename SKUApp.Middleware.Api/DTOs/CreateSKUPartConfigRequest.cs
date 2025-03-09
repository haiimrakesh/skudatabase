using System;
using System.ComponentModel.DataAnnotations;

namespace SKUApp.Middleware.Api.DTOs
{
    /// <summary>
    /// Represents the configuration for a SKU part.
    /// </summary>
    public class CreateSKUPartConfigRequest
    {
        /// <summary>
        /// Gets or sets the name of the SKU part configuration.
        /// </summary>
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the length of the SKU part.
        /// </summary>
        [Range(1, 6, ErrorMessage = "Length must be between 1 and 6.")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the generic name of the SKU part.
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
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
        /// Description of the SKU Part.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
    }
}