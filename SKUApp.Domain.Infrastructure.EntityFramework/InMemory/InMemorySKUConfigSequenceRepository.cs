using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.EntityFramework.Repositories;
using SKUApp.Domain.Infrastructure.Repositories;

namespace SKUApp.Domain.Infrastructure.EntityFramework.InMemory;

public class InMemorySKUConfigSequenceRepository : GenericRepository<SKUConfigSequence>, ISKUConfigSequenceRepository
{
    public InMemorySKUConfigSequenceRepository(InMemoryDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SKUConfigSequence>> GetSKUConfigSequenceByConfigIdAsync(int skyConfigId)
    {
        return await _context.SKUConfigSequences.Where(x => x.SKUConfigId == skyConfigId).ToListAsync();
    }

    public async Task<IEnumerable<SKUConfigSequence>> GetSKUConfigSequenceByPartConfigIdAsync(int skyConfigId)
    {
        return await _context.SKUConfigSequences.Where(x => x.SKUPartConfigId == skyConfigId).ToListAsync();
    }

}