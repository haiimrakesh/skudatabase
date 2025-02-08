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

    public async Task AddSKUConfigAsync(SKUConfig skuConfig)
    {
        await _unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
    }

    public async Task AddSKUSequenceAsync(SKUConfigSequence skuSequence)
    {
        await _unitOfWork.SKUConfigSequenceRepository.AddAsync(skuSequence);
    }

    public async Task DeleteSKUAsync(int id)
    {
        await _unitOfWork.SKURepository.DeleteAsync(await _unitOfWork.SKURepository.GetByIdAsync(id));
    }

    public async Task DeleteSKUConfigAsync(int id)
    {
        await _unitOfWork.SKUConfigRepository.DeleteAsync(await _unitOfWork.SKUConfigRepository.GetByIdAsync(id));
    }

    public async Task DeleteSKUSequenceAsync(int id)
    {
        await _unitOfWork.SKUConfigSequenceRepository.DeleteAsync(await _unitOfWork.SKUConfigSequenceRepository.GetByIdAsync(id));
    }

    public async Task<IEnumerable<SKUConfig>> GetAllSKUConfigsAsync()
    {
        return await _unitOfWork.SKUConfigRepository.GetAllAsync();
    }

    public async Task<IEnumerable<SKU>> GetAllSKUsAsync()
    {
        return await _unitOfWork.SKURepository.GetAllAsync();
    }

    public async Task<SKU> GetSKUByIdAsync(int id)
    {
        return await _unitOfWork.SKURepository.GetByIdAsync(id);
    }

    public Task<SKUConfig> GetSKUConfigByIdAsync(int id)
    {
        return _unitOfWork.SKUConfigRepository.GetByIdAsync(id);
    }

    public async Task ReorderSKUSequence(IEnumerable<SKUConfigSequence> skuSequence)
    {
        foreach (var skuseqItem in skuSequence)
        {
            await _unitOfWork.SKUConfigSequenceRepository.UpdateAsync(skuseqItem);
        }
    }
}