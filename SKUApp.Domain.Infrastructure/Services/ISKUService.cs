using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.ErrorHandling;

namespace SKUApp.Domain.Infrastructure.Services;

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
    Task<Result<SKU>> GetSKUByIdAsync(int id);

    /// <summary>
    /// Retrieves all SKUs.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of SKUs.</returns>
    Task<Result<IEnumerable<SKU>>> GetAllSKUsAsync();

    /// <summary>
    /// Adds a new SKU.
    /// </summary>
    /// <param name="sku">The SKU to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKU>> AddSKUAsync(SKU sku);

    /// <summary>
    /// Deletes a SKU by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the SKU to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKU>> DeleteSKUAsync(int id);
}