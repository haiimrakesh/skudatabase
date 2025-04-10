using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.DataContracts;

namespace SKUApp.Data.EFCore.InMemory;

public class InMemorySKUPartConfigRepository : GenericRepository<SKUPartConfig>, ISKUPartConfigRepository
{
    public InMemorySKUPartConfigRepository(InMemoryDbContext context) : base(context)
    {
    }
}
