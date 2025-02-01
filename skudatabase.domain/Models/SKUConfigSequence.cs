using System;

namespace skudatabase.domain.Models
{
    /// <summary>
    /// Represents the configuration sequence for SKU.
    /// </summary>
    public class SKUConfigSequence
    {
        /// <summary>
        /// Gets or sets the unique identifier for the SKU configuration sequence.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the SKU part configuration.
        /// </summary>
        public int SKUPartConfigId { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the SKU configuration.
        /// </summary>
        public int Sequence { get; set; }
    }
}