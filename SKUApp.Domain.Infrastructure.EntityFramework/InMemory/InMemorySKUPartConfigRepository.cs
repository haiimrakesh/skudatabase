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
}
