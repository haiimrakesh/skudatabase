using SKUApp.Domain.Entities;

namespace SKUApp.Domain.Infrastructure.Repositories;

public interface ISKUPartValuesRepository : IRepository<SKUPartValues>
{
    Task<IEnumerable<SKUPartValues>> GetSKUPartValuesByUniqueCode(string uniqueCode, int skyPartConfigId);
}