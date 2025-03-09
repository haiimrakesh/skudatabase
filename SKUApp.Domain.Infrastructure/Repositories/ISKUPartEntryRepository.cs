using SKUApp.Domain.Entities;

namespace SKUApp.Domain.Infrastructure.Repositories;

public interface ISKUPartEntryRepository : IRepository<SKUPartEntry>
{
    Task<IEnumerable<SKUPartEntry>> GetSKUPartEntriesByUniqueCode(string uniqueCode, int skyPartConfigId);
}