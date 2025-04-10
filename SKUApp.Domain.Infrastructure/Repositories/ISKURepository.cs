using SKUApp.Domain.Entities;

namespace SKUApp.Domain.Infrastructure.Repositories;

/// <summary>
/// Interface for SKU Part Configuration Repository.
/// </summary>
/// <typeparam name="SKUPartConfig">The type of the SKU part configuration entity.</typeparam>
public interface ISKURepository : IRepository<SKU>
{
}