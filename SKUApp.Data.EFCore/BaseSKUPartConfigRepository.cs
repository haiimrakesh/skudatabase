using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.DataContracts;

namespace SKUApp.Data.EFCore;

public class BaseSKUPartConfigRepository : GenericRepository<SKUPartConfig>, ISKUPartConfigRepository
{
    public BaseSKUPartConfigRepository(ISKUDbContext context) : base(context, context.SKUPartConfigs)
    {
    }
}
