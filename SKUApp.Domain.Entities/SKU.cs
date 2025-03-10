using System;

namespace SKUApp.Domain.Entities;

/// <summary>
/// Represents a SKU (Stock Keeping Unit) entity.
/// </summary>
public class SKU
{
    /// <summary>
    /// Gets or sets the unique identifier for the SKU.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the code for the SKU.
    /// </summary>
    public string SKUCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the image associated with the SKU.
    /// </summary>
    public byte[] Image { get; set; } = new byte[0];

    /// <summary>
    /// Gets or sets the identifier for the skill configuration associated with the SKU.
    /// </summary>
    public int SkillConfigId { get; set; }
}
