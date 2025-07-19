using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.DataContracts;

namespace SKUApp.Data.EFCore;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly ISKUDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ISKUDbContext context, DbSet<T> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        (_context as DbContext)!.Entry(entity).State = EntityState.Modified;
        await Task.FromResult(0);
    }

    public virtual async Task DeleteAsync(int id)
    {
        T? entity = await _dbSet.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException();
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await Task.FromResult(0);
        }
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate)
    {
        List<T>? Result = _dbSet.Where(predicate).ToList();
        return await Task.FromResult(Result) ?? throw new InvalidOperationException("Entity not found.");
    }
    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await Task.FromResult(0);
    }
}
