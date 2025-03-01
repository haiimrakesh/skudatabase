using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.Repositories;

namespace SKUApp.Domain.Infrastructure.UnitOfWork;

public interface ISKUUnitOfWork : IDisposable
{
    IRepository<SKU> SKURepository { get; }
    ISKUConfigRepository SKUConfigRepository { get; }
    IRepository<SKUConfigSequence> SKUConfigSequenceRepository { get; }
    ISKUPartConfigRepository SKUPartConfigRepository { get; }
    ISKUPartValuesRepository SKUPartValuesRepository { get; }

    Task<int> SaveChangesAsync();
}
