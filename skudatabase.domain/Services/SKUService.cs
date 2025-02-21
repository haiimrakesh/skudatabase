using skudatabase.domain.Infrastructure.Services;
using skudatabase.domain.Infrastructure.UnitOfWork;
using skudatabase.domain.Models;

namespace skudatabase.domain.Services;

public class SKUService : ISKUService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddSKUAsync(SKU sku)
    {
        await _unitOfWork.SKURepository.AddAsync(sku);
    }

    public async Task DeleteSKUAsync(int id)
    {
        await _unitOfWork.SKURepository.DeleteAsync(await _unitOfWork.SKURepository.GetByIdAsync(id));
    }

    public async Task<IEnumerable<SKU>> GetAllSKUsAsync()
    {
        return await _unitOfWork.SKURepository.GetAllAsync();
    }

    public async Task<SKU> GetSKUByIdAsync(int id)
    {
        return await _unitOfWork.SKURepository.GetByIdAsync(id);
    }
}