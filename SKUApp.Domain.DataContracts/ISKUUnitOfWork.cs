using SKUApp.Domain.Entities;
namespace SKUApp.Domain.DataContracts;

public interface ISKUUnitOfWork : IDisposable
{
    ISKURepository SKURepository { get; }
    ISKUConfigRepository SKUConfigRepository { get; }
    ISKUConfigSequenceRepository SKUConfigSequenceRepository { get; }
    ISKUPartConfigRepository SKUPartConfigRepository { get; }
    ISKUPartEntryRepository SKUPartEntryRepository { get; }

    Task<int> SaveChangesAsync();
}
