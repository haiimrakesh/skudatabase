using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.DataContracts;

namespace SKUApp.Data.EFCore;

public class BaseSKURepository : GenericRepository<SKU>, ISKURepository
{
    public BaseSKURepository(ISKUDbContext context) : base(context)
    {
    }
}
