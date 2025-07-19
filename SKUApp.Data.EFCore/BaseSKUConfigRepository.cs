using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.DataContracts;


namespace SKUApp.Data.EFCore;

public class BaseSKUConfigRepository : GenericRepository<SKUConfig>, ISKUConfigRepository
{
    public BaseSKUConfigRepository(ISKUDbContext context) : base(context, context.SKUConfigs)
    {
    }

    public async Task ActivateSKUConfig(int skuConfigId)
    {
        var skuConfig = await _context.SKUConfigs.FirstOrDefaultAsync(x => x.Id == skuConfigId);
        if (skuConfig != null)
        {
            skuConfig.Status = SKUConfigStatusEnum.Active;
        }
    }

    public async Task DeactivateSKUConfig(int skuConfigId)
    {
        var skuConfig = await _context.SKUConfigs.FirstOrDefaultAsync(x => x.Id == skuConfigId);
        if (skuConfig != null)
        {
            skuConfig.Status = SKUConfigStatusEnum.Discontinued;
        }
    }

    public async Task<bool> HasRelatedDataAsync(int skuConfigId)
    {
        var hasSKUConfigSequenceData = await _context.SKUConfigSequences.AnyAsync(x => x.SKUConfigId == skuConfigId);
        return hasSKUConfigSequenceData;
    }
}