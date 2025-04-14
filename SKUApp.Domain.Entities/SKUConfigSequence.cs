using System;

namespace SKUApp.Domain.Entities
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
        /// Gets or sets the identifier for the SKU configuration.
        /// </summary>
        public int SKUConfigId { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the SKU configuration.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Space to describe the relationship between the SKU configuration and the SKU part configuration.
        /// This could be used to provide additional context or information about the relationship.
        /// </summary>
        public string RelationshipDescription { get; set; } = string.Empty;

        public virtual SKUPartConfig? SKUPartConfig { get; set; } = null!;
        public virtual SKUConfig? SKUConfig { get; set; } = null!;
    }
}