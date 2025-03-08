
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.EntityFramework.Repositories;
using SKUApp.Domain.Infrastructure.Repositories;
using SKUApp.Domain.Infrastructure.UnitOfWork;

namespace SKUApp.Domain.Infrastructure.EntityFramework.InMemory
{
    public class InMemorySKUUnitOfWork : ISKUUnitOfWork
    {
        private readonly InMemoryDbContext _context;
        private bool _disposed;

        public InMemorySKUUnitOfWork(InMemoryDbContext context)
        {
            _context = context;
            SKURepository = new GenericRepository<SKU>(_context);
            SKUConfigRepository = new InMemorySKUConfigRepository(_context);
            SKUConfigSequenceRepository = new InMemorySKUConfigSequenceRepository(_context);
            SKUPartConfigRepository = new InMemorySKUPartConfigRepository(_context);
            SKUPartValuesRepository = new InMemorySKUPartValuesRepository(_context);
        }

        public IRepository<SKU> SKURepository { get; private set; }

        public ISKUConfigRepository SKUConfigRepository { get; private set; }

        public ISKUConfigSequenceRepository SKUConfigSequenceRepository { get; private set; }

        public ISKUPartConfigRepository SKUPartConfigRepository { get; private set; }

        public ISKUPartValuesRepository SKUPartValuesRepository { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            // In-memory implementation, so just return 0 changes
            //return await Task.FromResult(0);
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here
                    _context.Dispose();
                }

                // Dispose unmanaged resources here
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}