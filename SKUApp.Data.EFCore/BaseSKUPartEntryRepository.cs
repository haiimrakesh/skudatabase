using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.DataContracts;

namespace SKUApp.Data.EFCore;

public class BaseSKUPartEntryRepository : GenericRepository<SKUPartEntry>, ISKUPartEntryRepository
{
    public BaseSKUPartEntryRepository(ISKUDbContext context) : base(context)
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
