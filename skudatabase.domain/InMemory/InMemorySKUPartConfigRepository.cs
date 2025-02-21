using skudatabase.domain.Models;
using Microsoft.EntityFrameworkCore;
using skudatabase.domain.Infrastructure.Repositories;

namespace skudatabase.domain.InMemory;

public class InMemorySKUPartConfigRepository : GenericRepository<SKUPartConfig>, ISKUPartConfigRepository
{
    public InMemorySKUPartConfigRepository(InMemoryDbContext context) : base(context)
    {
    }

    public async Task ActivateSKUPartConfigBySKUConfigId(int skuConfigId)
    {
        await _context.SKUPartConfigs.Where(x => x.SKUConfigId == skuConfigId)
        .ForEachAsync(x => x.Status = SKUConfigStatusEnum.Active);
    }

    public async Task DeactivateSKUPartConfigBySKUConfigId(int skuConfigId)
    {
        await _context.SKUPartConfigs.Where(x => x.SKUConfigId == skuConfigId)
        .ForEachAsync(x => x.Status = SKUConfigStatusEnum.Discontinued);
    }
}
