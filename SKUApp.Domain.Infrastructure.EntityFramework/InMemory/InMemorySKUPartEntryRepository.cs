using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.EntityFramework.Repositories;
using SKUApp.Domain.Infrastructure.Repositories;
namespace SKUApp.Domain.Infrastructure.EntityFramework.InMemory;

public class InMemorySKUPartEntryRepository : GenericRepository<SKUPartEntry>, ISKUPartEntryRepository
{
    public InMemorySKUPartEntryRepository(InMemoryDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<SKUPartEntry>> GetSKUPartEntriesByUniqueCode(string uniqueCode, int skyPartConfigId)
    {
        List<SKUPartEntry> results = _context.SKUPartEntries.ToList();
        return await _context.SKUPartEntries.Where(v =>
        v.UniqueCode == uniqueCode && v.SKUPartConfigId == skyPartConfigId)
        .ToListAsync();
    }

}
