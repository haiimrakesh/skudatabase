using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using skudatabase.domain.Infrastructure.UnitOfWork;
using skudatabase.domain.Infrastructure.Repositories;
using skudatabase.domain.Models;

namespace skudatabase.MVC.Web.Database;

public class SKUSqliteUnitOfWork : ISKUUnitOfWork
{
    private readonly SKUDatabaseContext _context;
    private bool _disposed;

    public SKUSqliteUnitOfWork(SKUDatabaseContext context)
    {
        _context = context;
        SKURepository = new GenericRepository<SKU>(_context);
        SKUConfigRepository = new GenericRepository<SKUConfig>(_context);
        SKUConfigSequenceRepository = new GenericRepository<SKUConfigSequence>(_context);
        SKUPartConfigRepository = new GenericRepository<SKUPartConfig>(_context);
        SKUPartValuesRepository = new SKUPartValuesRepository(_context);
    }

    public IRepository<SKU> SKURepository { get; private set; }

    public IRepository<SKUConfig> SKUConfigRepository { get; private set; }

    public IRepository<SKUConfigSequence> SKUConfigSequenceRepository { get; private set; }

    public IRepository<SKUPartConfig> SKUPartConfigRepository { get; private set; }

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
