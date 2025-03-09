using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.Repositories;

namespace SKUApp.Domain.Infrastructure.UnitOfWork;

public interface ISKUUnitOfWork : IDisposable
{
    IRepository<SKU> SKURepository { get; }
    ISKUConfigRepository SKUConfigRepository { get; }
    ISKUConfigSequenceRepository SKUConfigSequenceRepository { get; }
    ISKUPartConfigRepository SKUPartConfigRepository { get; }
    ISKUPartEntryRepository SKUPartEntryRepository { get; }

    Task<int> SaveChangesAsync();
}
