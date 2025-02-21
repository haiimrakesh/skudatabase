using System;
using System.Threading.Tasks;
using skudatabase.domain.Models;
using skudatabase.domain.Infrastructure.Repositories;

namespace skudatabase.domain.Infrastructure.UnitOfWork;

public interface ISKUUnitOfWork : IDisposable
{
    IRepository<SKU> SKURepository { get; }
    IRepository<SKUConfig> SKUConfigRepository { get; }
    IRepository<SKUConfigSequence> SKUConfigSequenceRepository { get; }
    ISKUPartConfigRepository SKUPartConfigRepository { get; }
    ISKUPartValuesRepository SKUPartValuesRepository { get; }

    Task<int> SaveChangesAsync();
}
