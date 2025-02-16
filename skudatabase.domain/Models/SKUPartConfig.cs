using System;

namespace skudatabase.domain.Models
{
    /// <summary>
    /// Represents the configuration for a SKU part.
    /// </summary>
    public class SKUPartConfig
    {
        private int _length = 0;
        /// <summary>
        /// Gets or sets the unique identifier for the SKU part configuration.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the SKU part configuration.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the length of the SKU part.
        /// </summary>
        public int Length
        {
            get
            {
                return _length;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Length must be greater than 0");
                }
                if (value > 5)
                {
                    _length = 5;
                    throw new ArgumentException("Length must be lesser than 5");
                }
                _length = value;
            }
        }

        /// <summary>
        /// Gets or sets the generic name of the SKU part.
        /// </summary>
        public string GenericName { get; set; } = string.Empty;

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
        /// Gets or sets the SKU identifier associated with this configuration.
        /// </summary>
        public int SKUConfigId { get; set; }

        /// <summary>
        /// Indicates if a Hyphen is included at the end of the SKU Part entry. Increases the length by 1.
        /// </summary>
        public bool IncludeSpacerAtTheEnd { get; set; }

        /// <summary>
        /// Gets or sets the width of the SKU configuration.
        /// </summary>
        public SKUConfigStatusEnum Status { get; set; } = SKUConfigStatusEnum.Draft;

        /// <summary>
        /// Description of the SKU Part.
        /// </summary>
        public string Description { get; set; } = null!;
        public string GetDefaultGenericCode()
        {
            if (this.IsAlphaNumeric)
            {
                return new string('Z', this.Length);
            }
            else
            {
                return new string('9', this.Length);
            }
        }
    }
}