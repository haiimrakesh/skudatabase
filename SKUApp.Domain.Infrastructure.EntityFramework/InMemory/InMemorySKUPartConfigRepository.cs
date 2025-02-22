using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.EntityFramework.Repositories;
using SKUApp.Domain.Infrastructure.Repositories;

namespace SKUApp.Domain.Infrastructure.EntityFramework.InMemory;

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
