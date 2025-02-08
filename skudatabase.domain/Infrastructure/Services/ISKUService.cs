using skudatabase.domain.Models;

namespace skudatabase.domain.Infrastructure.Services;

/// <summary>
/// Interface for SKU service operations.
/// </summary>
public interface ISKUService
{
    /// <summary>
    /// Retrieves a SKU by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the SKU.</returns>
    Task<SKU> GetSKUByIdAsync(int id);

    /// <summary>
    /// Retrieves all SKUs.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of SKUs.</returns>
    Task<IEnumerable<SKU>> GetAllSKUsAsync();

    /// <summary>
    /// Adds a new SKU.
    /// </summary>
    /// <param name="sku">The SKU to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddSKUAsync(SKU sku);

    /// <summary>
    /// Deletes a SKU by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteSKUAsync(int id);

    /// <summary>
    /// Retrieves a SKU configuration by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU configuration.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the SKU configuration.</returns>
    Task<SKUConfig> GetSKUConfigByIdAsync(int id);

    /// <summary>
    /// Retrieves all SKU configurations.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of SKU configurations.</returns>
    Task<IEnumerable<SKUConfig>> GetAllSKUConfigsAsync();

    /// <summary>
    /// Adds a new SKU configuration.
    /// </summary>
    /// <param name="skuConfig">The SKU configuration to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddSKUConfigAsync(SKUConfig skuConfig);

    /// <summary>
    /// Deletes a SKU configuration by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU configuration to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteSKUConfigAsync(int id);

    /// <summary>
    /// Adds a new SKU sequence.
    /// </summary>
    /// <param name="skuSequence">The SKU sequence to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddSKUSequenceAsync(SKUConfigSequence skuSequence);

    /// <summary>
    /// Deletes a SKU sequence by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU sequence to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteSKUSequenceAsync(int id);

    /// <summary>
    /// Reorders the SKU sequences.
    /// </summary>
    /// <param name="skuSequence">The collection of SKU sequences to reorder.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ReorderSKUSequence(IEnumerable<SKUConfigSequence> skuSequence);
}