using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.ErrorHandling;

namespace SKUApp.Domain.Services;

/// <summary>
/// Interface for SKU service operations.
/// </summary>
public interface ISKUConfigService
{
    /// <summary>
    /// Retrieves a SKU configuration by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU configuration.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the SKU configuration.</returns>
    Task<Result<SKUConfig>> GetSKUConfigByIdAsync(int id);

    /// <summary>
    /// Retrieves all SKU configurations.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of SKU configurations.</returns>
    Task<Result<IEnumerable<SKUConfig>>> GetAllSKUConfigsAsync();

    /// <summary>
    /// Adds a new SKU configuration.
    /// </summary>
    /// <param name="skuConfig">The SKU configuration to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfig>> AddSKUConfigAsync(SKUConfig skuConfig);

    /// <summary>
    /// Deletes a SKU configuration by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU configuration to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfig>> DeleteSKUConfigAsync(int id);

    /// <summary>
    /// Adds a new SKU sequence.
    /// </summary>
    /// <param name="skuSequence">The SKU sequence to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigSequence>> AddSKUSequenceAsync(SKUConfigSequence skuSequence);

    /// <summary>
    /// Deletes a SKU sequence by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU sequence to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigSequence>> DeleteSKUSequenceAsync(int id);

    /// <summary>
    /// Reorders the SKU sequences.
    /// </summary>
    /// <param name="skuSequence">The collection of SKU sequences to reorder.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<IEnumerable<SKUConfigSequence>>> ReorderSKUSequence(IEnumerable<SKUConfigSequence> skuSequence);

    /// <summary>
    /// Activates a SKU configuration.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<SKUConfig>> ActivateSKUConfigAsync(int id);

    /// <summary>
    /// Deactivates a SKU configuration.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<SKUConfig>> DeactivateSKUConfigAsync(int id);
}