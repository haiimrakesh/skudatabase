using SKUApp.Presentation.DataTransferObjects.ViewModels;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Common.ErrorHandling;

namespace SKUApp.Domain.ServiceContracts;

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
    Task<Result<SKUConfigViewModel>> GetSKUConfigByIdAsync(int id, bool includeSKUPartConfig = false);

    /// <summary>
    /// Retrieves all SKU configurations.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of SKU configurations.</returns>
    Task<Result<IEnumerable<SKUConfigViewModel>>> GetAllSKUConfigsAsync();

    /// <summary>
    /// Adds a new SKU configuration.
    /// </summary>
    /// <param name="skuConfig">The SKU configuration to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigViewModel>> AddSKUConfigAsync(CreateSKUConfigRequest skuConfig);

    /// <summary>
    /// Adds a new SKU configuration.
    /// </summary>
    /// <param name="skuConfig">The SKU configuration to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigViewModel>> UpdateSKUConfigAsync(UpdateSKUConfigRequest skuConfig);

    /// <summary>
    /// Deletes a SKU configuration by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU configuration to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigViewModel>> DeleteSKUConfigAsync(int id);

    /// <summary>
    /// Activates a SKU configuration.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<SKUConfigViewModel>> ActivateSKUConfigAsync(int id);

    /// <summary>
    /// Deactivates a SKU configuration.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<SKUConfigViewModel>> DeactivateSKUConfigAsync(int id);
}