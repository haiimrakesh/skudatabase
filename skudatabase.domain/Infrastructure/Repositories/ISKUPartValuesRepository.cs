using skudatabase.domain.Models;

namespace skudatabase.domain.Infrastructure.Repositories;

public interface ISKUPartValuesRepository : IRepository<SKUPartValues>
{
    Task<IEnumerable<SKUPartValues>> GetSKUPartValuesByUniqueCode(string uniqueCode, int skyPartConfigId);
}