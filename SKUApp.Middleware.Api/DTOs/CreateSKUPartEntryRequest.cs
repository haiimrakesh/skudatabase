namespace SKUApp.Middleware.Api.DTOs
{
    /// <summary>
    /// Represents the SKU part values.
    /// </summary>
    public class CreateSKUPartEntryRequest
    {
        /// <summary>
        /// Gets or sets the unique code for the SKU part value.
        /// </summary>
        public string UniqueCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the SKU part value.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the SKU part configuration identifier.
        /// </summary>
        public int SKUPartConfigId { get; set; }
    }
}