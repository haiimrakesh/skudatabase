using SKUApp.Domain.Entities;
namespace SKUApp.Domain.DataContracts;
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

    /// <summary>
    /// Checks if the SKU configuration with the specified identifier has related data.
    /// </summary>
    /// <param name="skuConfigId"></param>
    /// <returns></returns>
    Task<bool> HasRelatedDataAsync(int skuConfigId);
}