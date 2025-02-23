using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.ErrorHandling;
using SKUApp.Domain.Infrastructure.UnitOfWork;

namespace SKUApp.Domain.Services;

public class SKUConfigService : ISKUConfigService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUConfigService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task<Result<SKUConfig>> CheckSKUStatus(int id, SKUConfigStatusEnum status)
    {
        try
        {
            var configResult = await this.GetSKUConfigByIdAsync(id);
            if (!configResult.IsSuccess)
            {
                return configResult;
            }

            if (configResult.Value?.Status == status)
            {
                return Error.BadRequest($"Changes not allowed in the current status.");
            }

            return configResult;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUConfig>> AddSKUConfigAsync(SKUConfig skuConfig)
    {
        try
        {
            // skuConfig while adding is always in draft status
            skuConfig.Status = SKUConfigStatusEnum.Draft;
            if (skuConfig.Length < 0 || skuConfig.Length > 25)
            {
                return Error.BadRequest("Length must be between 0 and 25");
            }

            if (string.IsNullOrEmpty(skuConfig.Name))
            {
                return Error.BadRequest("SKUName cannot be null or empty");
            }

            await _unitOfWork.SKUConfigRepository.AddAsync(skuConfig);

            return skuConfig;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUConfigSequence>> AddSKUSequenceAsync(SKUConfigSequence skuSequence)
    {
        try
        {
            var result = await this.CheckSKUStatus(skuSequence.SKUPartConfigId, SKUConfigStatusEnum.Draft);
            if (!result.IsSuccess)
            {
                return result.Error;
            }
            if (skuSequence.Sequence < 0 || skuSequence.Sequence > 25)
            {
                return Error.BadRequest("Sequence must be between 0 and 25");
            }

            SKUPartConfig? sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(skuSequence.SKUPartConfigId);
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found");
            }

            await _unitOfWork.SKUConfigSequenceRepository.AddAsync(skuSequence);

            return skuSequence;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUConfig>> DeleteSKUConfigAsync(int id)
    {
        try
        {
            var result = await this.CheckSKUStatus(id, SKUConfigStatusEnum.Active);
            if (!result.IsSuccess)
            {
                return result.Error;
            }
            await _unitOfWork.SKUConfigRepository.DeleteAsync(result.Value!);
            return result.Value!;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }

    public async Task<Result<SKUConfigSequence>> DeleteSKUSequenceAsync(int id)
    {
        try
        {
            var skuSquence = await _unitOfWork.SKUConfigSequenceRepository.GetByIdAsync(id);
            if (skuSquence == null)
            {
                return Error.NotFound("SKUConfigSequence not found");
            }

            var result = await this.CheckSKUStatus(skuSquence.SKUConfigId, SKUConfigStatusEnum.Active);
            if (!result.IsSuccess)
            {
                return result.Error;
            }
            await _unitOfWork.SKUConfigSequenceRepository.DeleteAsync(skuSquence);

            return skuSquence;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }

    public async Task<Result<IEnumerable<SKUConfig>>> GetAllSKUConfigsAsync()
    {
        try
        {
            List<SKUConfig> skuConfigs = (await _unitOfWork.SKUConfigRepository.GetAllAsync()).ToList();
            if (skuConfigs.Count == 0)
            {
                return Error.NotFound("No SKUConfigs found");
            }
            return skuConfigs;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }

    public async Task<Result<SKUConfig>> GetSKUConfigByIdAsync(int id)
    {
        try
        {
            var skuConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(id);
            if (skuConfig == null)
            {
                return Error.NotFound("SKUConfig not found");
            }
            return skuConfig;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }

    public async Task<Result<IEnumerable<SKUConfigSequence>>> ReorderSKUSequenceAsync(IEnumerable<SKUConfigSequence> skuSequence)
    {
        try
        {
            bool isSKUCheckCompleted = false;
            foreach (var skuseqItem in skuSequence)
            {
                if (!isSKUCheckCompleted)
                {
                    var result = await this.CheckSKUStatus(skuseqItem.SKUPartConfigId, SKUConfigStatusEnum.Active);
                    if (!result.IsSuccess)
                    {
                        return result.Error;
                    }
                    isSKUCheckCompleted = true;
                }
                await _unitOfWork.SKUConfigSequenceRepository.UpdateAsync(skuseqItem);
            }

            return skuSequence.ToList();
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }

    public async Task<Result<SKUConfig>> ActivateSKUConfigAsync(int id)
    {
        try
        {
            var skuConfigResult = await this.CheckSKUStatus(id, SKUConfigStatusEnum.Active);
            if (!skuConfigResult.IsSuccess)
            {
                return skuConfigResult;
            }
            if (skuConfigResult.Value?.Status == SKUConfigStatusEnum.Discontinued)
            {
                return Error.BadRequest("Cannot activate a SKUConfig in Discontinued status");
            }
            //Set the status to Active
            var skuConfig = skuConfigResult.Value;
            skuConfig!.Status = SKUConfigStatusEnum.Active;
            await _unitOfWork.SKUConfigRepository.UpdateAsync(skuConfig);
            //Set status of all related SKU Parts to Active
            await _unitOfWork.SKUPartConfigRepository.ActivateSKUPartConfigBySKUConfigId(id);

            return skuConfig;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }

    public async Task<Result<SKUConfig>> DeactivateSKUConfigAsync(int id)
    {
        try
        {

            var skuConfigResult = await this.CheckSKUStatus(id, SKUConfigStatusEnum.Discontinued);
            if (!skuConfigResult.IsSuccess)
            {
                return skuConfigResult;
            }
            //Set the status to Active
            var skuConfig = skuConfigResult.Value;
            skuConfig!.Status = SKUConfigStatusEnum.Discontinued;
            await _unitOfWork.SKUConfigRepository.UpdateAsync(skuConfig);
            //Set status of all related SKU Parts to Active
            await _unitOfWork.SKUPartConfigRepository.DeactivateSKUPartConfigBySKUConfigId(id);

            return skuConfig;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }
}