namespace SKUApp.Domain.Entities
{
    /// <summary>
    /// Represents the configuration status of a SKU (Stock Keeping Unit).
    /// </summary>
    public enum SKUConfigStatusEnum
    {
        /// <summary>
        /// Indicates that the SKU configuration is in draft state and not finalized.
        /// </summary>
        Draft = 0,

        /// <summary>
        /// Indicates that the SKU configuration is active and available for use.
        /// </summary>
        Active,

        /// <summary>
        /// Indicates that the SKU configuration has been discontinued and is no longer in use.
        /// </summary>
        Discontinued
    }
}