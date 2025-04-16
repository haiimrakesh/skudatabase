using SKUApp.Presentation.DataTransferObjects.ViewModels;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Common.ErrorHandling;

namespace SKUApp.Domain.ServiceContracts;

/// <summary>
/// Interface for SKUPartConfigService.
/// </summary>
public interface ISKUPartEntryService
{
    /// <summary>
    /// Adds a new SKUPartValue.
    /// </summary>
    /// <param name="sKUPartValues">The SKUPartValues to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the SKUPartConfig is active or a SKUPartValue with the same UniqueCode already exists.
    /// </exception>
    Task<Result<SKUPartEntryViewModel>> AddSKUPartEntryAsync(CreateSKUPartEntryRequest sKUPartEntry);
    /// <summary>
    /// Update a SKUPartValue.
    /// </summary>
    /// <param name="sKUPartValues">The SKUPartValues to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the SKUPartConfig is active or a SKUPartValue with the same UniqueCode already exists.
    /// </exception>
    Task<Result<SKUPartEntryViewModel>> UpdateSKUPartEntryAsync(UpdateSKUPartEntryRequest sKUPartEntry);

    /// <summary>
    /// Deletes an existing SKUPartValue.
    /// </summary>
    /// <param name="id">The ID of the SKUPartValue to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the SKUPartValues is not found or the SKUPartConfig is active.
    /// </exception>
    Task<Result<SKUPartEntryViewModel>> DeleteSKUPartEntryAsync(int id);
}
