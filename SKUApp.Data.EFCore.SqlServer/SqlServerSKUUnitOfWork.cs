
using SKUApp.Domain.Entities;
using SKUApp.Domain.DataContracts;

namespace SKUApp.Data.EFCore.SqlServer
{
    public class SqlServerSKUUnitOfWork : ISKUUnitOfWork
    {
        private readonly SqlServerDbContext _context;
        private bool _disposed;

        public SqlServerSKUUnitOfWork(SqlServerDbContext context)
        {
            _context = context;
            SKURepository = new BaseSKURepository(_context);
            SKUConfigRepository = new BaseSKUConfigRepository(_context);
            SKUConfigSequenceRepository = new BaseSKUConfigSequenceRepository(_context);
            SKUPartConfigRepository = new BaseSKUPartConfigRepository(_context);
            SKUPartEntryRepository = new BaseSKUPartEntryRepository(_context);
        }

        public ISKURepository SKURepository { get; private set; }

        public ISKUConfigRepository SKUConfigRepository { get; private set; }

        public ISKUConfigSequenceRepository SKUConfigSequenceRepository { get; private set; }

        public ISKUPartConfigRepository SKUPartConfigRepository { get; private set; }

        public ISKUPartEntryRepository SKUPartEntryRepository { get; private set; }

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