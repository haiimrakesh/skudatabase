using SKUApp.Presentation.DataTransferObjects.ViewModels;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Common.ErrorHandling;

namespace SKUApp.Domain.ServiceContracts;

/// <summary>
/// Interface for SKU service operations.
/// </summary>
public interface ISKUConfigSequenceService
{
    /// <summary>
    /// Adds a new SKU sequence.
    /// </summary>
    /// <param name="skuSequence">The SKU sequence to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigSequenceViewModel>> AddSKUSequenceAsync(CreateSKUConfigSequenceRequest skuSequence);

    /// <summary>
    /// Adds a new SKU sequence.
    /// </summary>
    /// <param name="skuSequence">The SKU sequence to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigSequenceViewModel>> UpdateSKUSequenceAsync(UpdateSKUConfigSequenceRequest skuSequence);
    /// <summary>
    /// Deletes a SKU sequence by its identifier.
    /// </summary>
    /// <param name="skuSequenceId">The identifier of the SKU sequence to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigSequenceViewModel>> DeleteSKUSequenceAsync(int skuSequenceId);

    /// <summary>
    /// Reorders the SKU sequences.
    /// </summary>
    /// <param name="skuSequence">The collection of SKU sequences to reorder.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result<SKUConfigViewModel>> ReorderSKUSequenceAsync(ReOrderSKUConfigSequenceRequest reOrderSKUConfigSequenceRequest);
}