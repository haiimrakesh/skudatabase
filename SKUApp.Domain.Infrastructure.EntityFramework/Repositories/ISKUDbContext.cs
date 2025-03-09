using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;

namespace SKUApp.Domain.Infrastructure.EntityFramework.Repositories;
/// <summary>
/// Represents the database context interface for SKU-related entities.
/// </summary>
public interface ISKUDbContext
{
    /// <summary>
    /// Gets the DbSet of entities of the given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    DbSet<T>? GetDbSet<T>() where T : class;
    /// <summary>
    /// Gets or sets the DbSet of SKU entities.
    /// </summary>
    IQueryable<SKU> SKUs { get; }

    /// <summary>
    /// Gets or sets the DbSet of SKUPartConfig entities.
    /// </summary>
    IQueryable<SKUPartConfig> SKUPartConfigs { get; }

    /// <summary>
    /// Gets or sets the DbSet of SKUPartValues entities.
    /// </summary>
    IQueryable<SKUPartEntry> SKUPartEntries { get; }

    /// <summary>
    /// Gets or sets the DbSet of SKUConfig entities.
    /// </summary>
    IQueryable<SKUConfig> SKUConfigs { get; }

    /// <summary>
    /// Gets or sets the DbSet of SKUConfigSequence entities.
    /// </summary>
    IQueryable<SKUConfigSequence> SKUConfigSequences { get; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}