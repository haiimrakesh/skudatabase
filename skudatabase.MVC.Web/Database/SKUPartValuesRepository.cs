using skudatabase.domain.Models;
using Microsoft.EntityFrameworkCore;
using skudatabase.domain.Infrastructure.Repositories;

namespace skudatabase.MVC.Web.Database;

public class SKUPartValuesRepository : GenericRepository<SKUPartValues>, ISKUPartValuesRepository
{
    public SKUPartValuesRepository(SKUDatabaseContext context) : base(context)
    {
    }
    public async Task<IEnumerable<SKUPartValues>> GetSKUPartValuesByUniqueCode(string uniqueCode, int skyPartConfigId)
    {
        List<SKUPartValues> results = _context.SKUPartValues.ToList();
        return await _context.SKUPartValues.Where(v =>
        v.UniqueCode == uniqueCode && v.SKUPartConfigId == skyPartConfigId)
        .ToListAsync();
    }
}
