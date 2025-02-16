namespace skudatabase.domain.Models
{
    /// <summary>
    /// Represents the configuration for a SKU (Stock Keeping Unit).
    /// </summary>
    public class SKUConfig
    {
        /// <summary>
        /// Gets or sets the unique identifier for the SKU configuration.
        /// </summary>
        public int Id { get; set; }

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
        public SKUConfigStatusEnum Status { get; set; } = SKUConfigStatusEnum.Draft;

        /// <summary>
        /// Description of the SKU
        /// </summary>
        public string Description { get; set; } = null!;
    }
}