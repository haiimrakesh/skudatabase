using SKUApp.Domain.Entities;

namespace SKUApp.Domain.Infrastructure.Repositories;

/// <summary>
/// Interface for SKU Part Configuration Repository.
/// </summary>
/// <typeparam name="SKUPartConfig">The type of the SKU part configuration entity.</typeparam>
public interface ISKUPartConfigRepository : IRepository<SKUPartConfig>
{
    /// <summary>
    /// Activates the SKU part configuration by SKU configuration ID.
    /// </summary>
    /// <param name="skuConfigId">The SKU configuration ID.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ActivateSKUPartConfigBySKUConfigId(int skuConfigId);

    /// <summary>
    /// Deactivates the SKU part configuration by SKU configuration ID.
    /// </summary>
    /// <param name="skuConfigId">The SKU configuration ID.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeactivateSKUPartConfigBySKUConfigId(int skuConfigId);
}