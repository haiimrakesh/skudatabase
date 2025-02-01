using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skudatabase.domain.DataLayer;
using skudatabase.domain.Models;

namespace skudatabase.domain.Infrastructure
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly InMemoryDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(InMemoryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            T? entity = await _dbSet.FindAsync(id);
            if(entity == null)
                throw new KeyNotFoundException();

            return entity;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(0);
        }

        public virtual async Task DeleteAsync(int id)
        {            
            T? entity = await _dbSet.FindAsync(id);
            if(entity == null)
                throw new KeyNotFoundException();
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await Task.FromResult(0);
            }
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await Task.FromResult(0);
        }
    }
}