using skudatabase.domain.Infrastructure.Services;
using skudatabase.domain.Infrastructure.UnitOfWork;
using skudatabase.domain.Models;

namespace skudatabase.domain.Services;

public class SKUConfigService : ISKUConfigService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUConfigService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Throws exception if the SKUConfig doesn't exist or if it is not in the specified status.
    /// </summary>
    /// <param name="id">SKU Config Id</param>
    /// <param name="status">Status to be checked against </param>
    /// <returns></returns>
    private async Task<SKUConfig> CheckSKUStatus(int id, SKUConfigStatusEnum status)
    {
        SKUConfig config = await this.GetSKUConfigByIdAsync(id);
        if (config == null)
        {
            throw new ArgumentNullException("SKUConfig not found");
        }

        if (config.Status == status)
        {
            throw new InvalidOperationException("Changes not allowed in the current status.");
        }

        return config;
    }

    public async Task AddSKUConfigAsync(SKUConfig skuConfig)
    {
        // skuConfig while adding is always in draft status
        skuConfig.Status = SKUConfigStatusEnum.Draft;
        if (skuConfig.Length < 0 || skuConfig.Length > 25)
        {
            throw new ArgumentOutOfRangeException("Length must be between 0 and 25");
        }

        if (string.IsNullOrEmpty(skuConfig.Name))
        {
            throw new ArgumentNullException("SKUName cannot be null or empty");
        }

        await _unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
    }

    public async Task AddSKUSequenceAsync(SKUConfigSequence skuSequence)
    {
        await this.CheckSKUStatus(skuSequence.SKUPartConfigId, SKUConfigStatusEnum.Draft);

        if (skuSequence.Sequence < 0 || skuSequence.Sequence > 25)
        {
            throw new ArgumentOutOfRangeException("Sequence must be between 0 and 25");
        }

        SKUPartConfig sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(skuSequence.SKUPartConfigId);
        if (sKUPartConfig == null)
        {
            throw new ArgumentNullException("SKUPartConfig not found");
        }

        await _unitOfWork.SKUConfigSequenceRepository.AddAsync(skuSequence);
    }

    public async Task DeleteSKUConfigAsync(int id)
    {
        await this.CheckSKUStatus(id, SKUConfigStatusEnum.Active);
        await _unitOfWork.SKUConfigRepository.DeleteAsync(await _unitOfWork.SKUConfigRepository.GetByIdAsync(id));
    }

    public async Task DeleteSKUSequenceAsync(int id)
    {
        await this.CheckSKUStatus(id, SKUConfigStatusEnum.Active);
        await _unitOfWork.SKUConfigSequenceRepository.DeleteAsync(await _unitOfWork.SKUConfigSequenceRepository.GetByIdAsync(id));
    }

    public async Task<IEnumerable<SKUConfig>> GetAllSKUConfigsAsync()
    {
        return await _unitOfWork.SKUConfigRepository.GetAllAsync();
    }

    public async Task<SKUConfig> GetSKUConfigByIdAsync(int id)
    {
        return await _unitOfWork.SKUConfigRepository.GetByIdAsync(id);
    }

    public async Task ReorderSKUSequence(IEnumerable<SKUConfigSequence> skuSequence)
    {
        bool isSKUCheckCompleted = false;
        foreach (var skuseqItem in skuSequence)
        {
            if (!isSKUCheckCompleted)
            {
                await this.CheckSKUStatus(skuseqItem.SKUPartConfigId, SKUConfigStatusEnum.Active);
                isSKUCheckCompleted = true;
            }
            await _unitOfWork.SKUConfigSequenceRepository.UpdateAsync(skuseqItem);
        }
    }

    public async Task ActivateSKUConfigAsync(int id)
    {
        var skuConfig = await this.CheckSKUStatus(id, SKUConfigStatusEnum.Active);
        if (skuConfig.Status == SKUConfigStatusEnum.Discontinued)
        {
            throw new InvalidOperationException("Cannot activate a SKUConfig in Discontinued status");
        }
        //Set the status to Active
        skuConfig.Status = SKUConfigStatusEnum.Active;
        await _unitOfWork.SKUConfigRepository.UpdateAsync(skuConfig);
        //Set status of all related SKU Parts to Active
        await _unitOfWork.SKUPartConfigRepository.ActivateSKUPartConfigBySKUConfigId(id);

    }

    public async Task DeactivateSKUConfigAsync(int id)
    {
        var skuConfig = await this.CheckSKUStatus(id, SKUConfigStatusEnum.Discontinued);
        //Set the status to Active
        skuConfig.Status = SKUConfigStatusEnum.Discontinued;
        await _unitOfWork.SKUConfigRepository.UpdateAsync(skuConfig);
        //Set status of all related SKU Parts to Active
        await _unitOfWork.SKUPartConfigRepository.DeactivateSKUPartConfigBySKUConfigId(id);
    }
}