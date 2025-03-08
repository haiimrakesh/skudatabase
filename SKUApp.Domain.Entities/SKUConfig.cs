using System.ComponentModel.DataAnnotations;

namespace SKUApp.Domain.Entities
{
    /// <summary>
    /// Represents the configuration for a SKU (Stock Keeping Unit).
    /// </summary>
    public class SKUConfig
    {
        /// <summary>
        /// Gets or sets the unique identifier for the SKU configuration.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the SKU configuration.
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the length of the SKU configuration.
        /// </summary>
        [Range(3, 25, ErrorMessage = "Length must be between 3 and 25.")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the width of the SKU configuration.
        /// </summary>
        [Required]
        public SKUConfigStatusEnum Status { get; set; } = SKUConfigStatusEnum.Draft;

        /// <summary>
        /// Description of the SKU
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 100 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}