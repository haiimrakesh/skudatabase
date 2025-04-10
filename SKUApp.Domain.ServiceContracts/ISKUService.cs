using SKUApp.Presentation.DataTransferObjects.ViewModels;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Common.ErrorHandling;

namespace SKUApp.Domain.Infrastructure.Services;

/// <summary>
/// Interface for SKU service operations.
/// </summary>
public interface ISKUService
{
    /// <summary>
    /// Retrieves a SKU configuration by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU configuration.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the SKU configuration.</returns>
    Task<Result<SKUViewModel>> GetSKUByIdAsync(int id, bool includeSKUPartConfig = false);

    /// <summary>
    /// Retrieves all SKU configurations.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of SKU configurations.</returns>
    Task<Result<IEnumerable<SKUViewModel>>> GetAllSKUsAsync();

    /// <summary>
    /// Adds a new SKU configuration.
    /// </summary>
    /// <param name="skuConfig">The SKU configuration to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUViewModel>> AddSKUAsync(CreateSKURequest skuConfig);

    /// <summary>
    /// Updates an existing SKU configuration.
    /// </summary>
    /// <param name="skuConfig">The SKU configuration to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUViewModel>> UpdateSKUAsync(UpdateSKURequest skuConfig);

    /// <summary>
    /// Deletes a SKU configuration by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU configuration to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUViewModel>> DeleteSKUAsync(int id);
}