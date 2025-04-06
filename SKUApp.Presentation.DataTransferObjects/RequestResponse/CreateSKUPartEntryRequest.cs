using System.ComponentModel.DataAnnotations;

namespace SKUApp.Presentation.DataTransferObjects.RequestResponse
{
    /// <summary>
    /// Represents the SKU part values.
    /// </summary>
    public class CreateSKUPartEntryRequest
    {
        /// <summary>
        /// Gets or sets the unique code for the SKU part value.
        /// </summary>
        [Required]
        public string UniqueCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the SKU part value.
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the SKU part configuration identifier.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "SKU Part Config ID must be a positive integer.")]
        public int SKUPartConfigId { get; set; }
    }
}