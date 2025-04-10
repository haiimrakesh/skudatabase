using SKUApp.Presentation.DataTransferObjects.ViewModels;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Common.ErrorHandling;

namespace SKUApp.Domain.Infrastructure.Services;

/// <summary>
/// Interface for SKUPartConfigService.
/// </summary>
public interface ISKUPartConfigService
{
    /// <summary>
    /// Gets a SKUPartConfig by ID.
    /// </summary>
    /// <param name="id">The ID of the SKUPartConfig to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the SKUPartConfig.</returns>
    Task<Result<SKUPartConfigViewModel>> GetSKUPartConfigByIdAsync(int id, bool includeSKUPartEntries = false);
    /// <summary>
    /// Gets a SKUPartConfigs
    /// </summary>
    /// <returns></returns>
    Task<Result<IEnumerable<SKUPartConfigViewModel>>> GetAllSKUPartConfigsAsync();
    /// <summary>
    /// Adds a new SKUPartConfig and its default SKUPartValues.
    /// </summary>
    /// <param name="sKUPartConfig">The SKUPartConfig to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUPartConfigViewModel>> AddSKUPartConfigAsync(CreateSKUPartConfigRequest sKUPartConfig);

    /// <summary>
    /// Deletes an existing SKUPartConfig.
    /// </summary>
    /// <param name="id">The ID of the SKUPartConfig to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the SKUPartConfig is not found, is active, has SKUPartValues, or is part of a SKU.
    /// </exception>
    Task<Result<SKUPartConfigViewModel>> DeleteSKUPartConfigAsync(int id);
}
