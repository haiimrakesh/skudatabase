using System;
using System.Threading.Tasks;
using skudatabase.domain.Models;

namespace skudatabase.domain.Infrastructure
{
    public interface ISKUUnitOfWork : IDisposable
    {
        IRepository<SKU> SKURepository { get; }
        IRepository<SKUConfig> SKUConfigRepository { get; }
        IRepository<SKUConfigSequence> SKUConfigSequenceRepository { get; }
        IRepository<SKUPartConfig> SKUPartConfigRepository { get; }
        ISKUPartValuesRepository SKUPartValuesRepository { get; }

        Task<int> SaveChangesAsync();
    }
}