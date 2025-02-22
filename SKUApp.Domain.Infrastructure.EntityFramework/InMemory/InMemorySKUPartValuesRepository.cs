using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.EntityFramework.Repositories;
using SKUApp.Domain.Infrastructure.Repositories;
namespace SKUApp.Domain.Infrastructure.EntityFramework.InMemory;

public class InMemorySKUPartValuesRepository : GenericRepository<SKUPartValues>, ISKUPartValuesRepository
{
    public InMemorySKUPartValuesRepository(InMemoryDbContext context) : base(context)
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
