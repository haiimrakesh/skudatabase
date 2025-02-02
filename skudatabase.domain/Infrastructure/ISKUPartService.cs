using skudatabase.domain.Models;

namespace skudatabase.domain.Infrastructure
{
    /// <summary>
    /// Interface for SKUPartConfigService.
    /// </summary>
    public interface ISKUPartConfigService
    {
        /// <summary>
        /// Adds a new SKUPartConfig and its default SKUPartValues.
        /// </summary>
        /// <param name="sKUPartConfig">The SKUPartConfig to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddSKUPartConfig(SKUPartConfig sKUPartConfig);

        /// <summary>
        /// Deletes an existing SKUPartConfig.
        /// </summary>
        /// <param name="id">The ID of the SKUPartConfig to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the SKUPartConfig is not found, is active, has SKUPartValues, or is part of a SKU.
        /// </exception>
        Task DeleteSKUPartConfig(int id);

        /// <summary>
        /// Adds a new SKUPartValue.
        /// </summary>
        /// <param name="sKUPartValues">The SKUPartValues to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the SKUPartConfig is active or a SKUPartValue with the same UniqueCode already exists.
        /// </exception>
        Task AddSKUPartValue(SKUPartValues sKUPartValues);

        /// <summary>
        /// Deletes an existing SKUPartValue.
        /// </summary>
        /// <param name="id">The ID of the SKUPartValue to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the SKUPartValues is not found or the SKUPartConfig is active.
        /// </exception>
        Task DeleteSKUPartValue(int id);
    }
}