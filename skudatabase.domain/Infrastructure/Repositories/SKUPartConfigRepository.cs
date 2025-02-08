using skudatabase.domain.Models;
using skudatabase.domain.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace skudatabase.domain.Infrastructure.Repositories;

public interface ISKUPartValuesRepository : IRepository<SKUPartValues>
{
    Task<IEnumerable<SKUPartValues>> GetSKUPartValuesByUniqueCode(string uniqueCode, int skyPartConfigId);
}

public class SKUPartValuesRepository : GenericRepository<SKUPartValues>, ISKUPartValuesRepository
{
    public SKUPartValuesRepository(InMemoryDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<SKUPartValues>> GetSKUPartValuesByUniqueCode(string uniqueCode, int skyPartConfigId)
    {
        List<SKUPartValues> results = _context.SKUPartValues.ToList();
        return await _context.SKUPartValues.Where(v =>
        v.UniqueCode == uniqueCode && v.SKUPartConfigId == skyPartConfigId)
        .ToListAsync();
    }
}
