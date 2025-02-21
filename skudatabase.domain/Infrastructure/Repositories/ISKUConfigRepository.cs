using skudatabase.domain.Infrastructure.Repositories;
using skudatabase.domain.Models;


/// <summary>
/// Interface for SKU configuration repository.
/// </summary>
public interface ISKUConfigRepository : IRepository<SKUConfig>
{
    /// <summary>
    /// Activates the SKU configuration with the specified identifier.
    /// </summary>
    /// <param name="skuConfigId">The identifier of the SKU configuration to activate.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ActivateSKUConfig(int skuConfigId);

    /// <summary>
    /// Decommissions the SKU configuration with the specified identifier.
    /// </summary>
    /// <param name="skuConfigId">The identifier of the SKU configuration to decommission.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeactivateSKUConfig(int skuConfigId);
}