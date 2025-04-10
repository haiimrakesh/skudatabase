using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.DataContracts;

namespace SKUApp.Data.EFCore.InMemory;

public class InMemorySKURepository : GenericRepository<SKU>, ISKURepository
{
    public InMemorySKURepository(InMemoryDbContext context) : base(context)
    {
    }
}
