using SKUApp.Domain.Entities;
using SKUApp.Common.ErrorHandling;
using SKUApp.Domain.DataContracts;
using SKUApp.Presentation.DataTransferObjects.ViewModels;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Common.Validation;
using SKUApp.Domain.ServiceContracts;

namespace SKUApp.Domain.Services;

public class SKUConfigService : ISKUConfigService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUConfigService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<SKUConfigViewModel>> GetSKUConfigByIdAsync(int id, bool includeSKUPartConfig = false)
    {
        try
        {
            var skuConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(id);
            if (skuConfig == null)
            {
                return Error.NotFound("SKUConfig not found");
            }

            var skuConfigViewModel = new SKUConfigViewModel
            {
                SKUConfigId = skuConfig.Id,
                Name = skuConfig.Name,
                Description = skuConfig.Description,
                Length = skuConfig.Length,
                Status = skuConfig.Status.ToString()
            };
            if (includeSKUPartConfig)
            {
                var skuSequences = await _unitOfWork.SKUConfigSequenceRepository.GetSKUConfigSequenceByConfigIdAsync(id);
                skuConfigViewModel.SKUConfigSequenceViewModels = skuSequences.Select(s => new SKUConfigSequenceViewModel
                {
                    SKUConfigSequenceId = s.Id,
                    SKUPartConfigId = s.SKUPartConfigId,
                    Sequence = s.Sequence,
                    SKUConfigId = s.SKUConfigId,
                    SKUPartName = s.SKUPartConfig?.Name ?? string.Empty,
                    SKUPartLength = s.SKUPartConfig?.Length ?? 0
                }).ToList();
            }
            return skuConfigViewModel;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }
    public async Task<ServiceResult<IEnumerable<SKUConfigViewModel>>> GetAllSKUConfigsAsync()
    {
        try
        {
            List<SKUConfig> skuConfigs = (await _unitOfWork.SKUConfigRepository.GetAllAsync()).ToList();
            if (skuConfigs.Count == 0)
            {
                return Error.NotFound("No SKUConfigs found");
            }
            return skuConfigs.Select(s => new SKUConfigViewModel
            {
                SKUConfigId = s.Id,
                Name = s.Name,
                Description = s.Description,
                Length = s.Length,
                Status = s.Status.ToString()
            }).ToList();
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }
    public async Task<ServiceResult<SKUConfigViewModel>> AddSKUConfigAsync(CreateSKUConfigRequest skuConfigRequest)
    {
        try
        {
            Error validationError = Error.ValidationFailures();
            if (!ValidationHelper.Validate(skuConfigRequest, validationError))
            {
                return validationError;
            }

            var skuConfig = new SKUConfig
            {
                Id = 0,
                Name = skuConfigRequest.Name.ToUpper().Trim(),
                Description = skuConfigRequest.Description.Trim(),
                Length = skuConfigRequest.Length,
                Status = SKUConfigStatusEnum.Draft
            };

            //Check if a SKUConfig exists by the same name.
            var existing = await _unitOfWork.SKUConfigRepository.FindAsync(s => s.Name == skuConfig.Name);
            if (existing.Any())
            {
                return Error.BadRequest("SKUConfig with the same name already exists");
            }

            await _unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
            await _unitOfWork.SaveChangesAsync();

            return new SKUConfigViewModel
            {
                SKUConfigId = skuConfig.Id,
                Name = skuConfig.Name,
                Description = skuConfig.Description,
                Length = skuConfig.Length,
                Status = skuConfig.Status.ToString()
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }
    public async Task<ServiceResult<SKUConfigViewModel>> UpdateSKUConfigAsync(UpdateSKUConfigRequest skuConfigRequest)
    {
        try
        {
            Error validationError = Error.ValidationFailures();
            if (!ValidationHelper.Validate(skuConfigRequest, validationError))
            {
                return validationError;
            }

            //Check if a SKUConfig exists by the same name.
            var existing = await _unitOfWork.SKUConfigRepository.GetByIdAsync(skuConfigRequest.Id);
            if (existing == null)
            {
                return Error.NotFound("SKUConfig not found");
            }

            if (existing.Status != SKUConfigStatusEnum.Draft)
            {
                return Error.BadRequest("SKUConfig must be in Draft status to update");
            }

            //Check if a SKUConfig exists by the same name (Duplicate check)
            var dupCheck = await _unitOfWork.SKUConfigRepository.FindAsync(s => s.Name == skuConfigRequest.Name && s.Id != skuConfigRequest.Id);
            if (dupCheck.Any())
            {
                return Error.BadRequest("SKUConfig with the same name already exists");
            }


            existing.Name = skuConfigRequest.Name;
            existing.Description = skuConfigRequest.Description;
            existing.Length = skuConfigRequest.Length;

            await _unitOfWork.SKUConfigRepository.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();

            return new SKUConfigViewModel
            {
                SKUConfigId = existing.Id,
                Name = existing.Name,
                Description = existing.Description,
                Length = existing.Length,
                Status = existing.Status.ToString()
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }
    public async Task<ServiceResult<SKUConfigViewModel>> DeleteSKUConfigAsync(int id)
    {
        try
        {
            var skuConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(id);
            if (skuConfig == null)
            {
                return Error.NotFound("SKUConfig not found");
            }
            if (skuConfig.Status == SKUConfigStatusEnum.Active)
            {
                return Error.BadRequest("Cannot delete an active SKUConfig");
            }

            if (await _unitOfWork.SKUConfigRepository.HasRelatedDataAsync(id))
            {
                return Error.BadRequest("Cannot delete SKUConfig with related data");
            }

            await _unitOfWork.SKUConfigRepository.DeleteAsync(skuConfig);
            await _unitOfWork.SaveChangesAsync();
            return new SKUConfigViewModel
            {
                SKUConfigId = skuConfig.Id,
                Name = skuConfig.Name,
                Description = skuConfig.Description,
                Length = skuConfig.Length,
                Status = skuConfig.Status.ToString()
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }

    }
    public async Task<ServiceResult<SKUConfigViewModel>> ActivateSKUConfigAsync(int id)
    {
        try
        {
            var skuConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(id);
            if (skuConfig == null)
            {
                return Error.NotFound("SKUConfig not found");
            }

            if (skuConfig.Status == SKUConfigStatusEnum.Active)
            {
                return Error.BadRequest("SKUConfig is already active");
            }
            if (skuConfig.Status == SKUConfigStatusEnum.Discontinued)
            {
                return Error.BadRequest("SKUConfig is in discontinued status");
            }

            //Check if the Length of SKUConfig is equal to all the Sequence Parts combined.
            var skuConfigViewModel = await this.GetSKUConfigByIdAsync(id, true);
            if (skuConfigViewModel.IsSuccess)
            {
                var skuConfigSequences = skuConfigViewModel.Value!.SKUConfigSequenceViewModels;
                int totalLength = skuConfigSequences.Sum(s => s.SKUPartLength);
                if (totalLength != skuConfig.Length)
                {
                    return Error.BadRequest("SKUConfig length does not match the sum of its parts");
                }
            }

            skuConfig.Status = SKUConfigStatusEnum.Active;
            await _unitOfWork.SKUConfigRepository.UpdateAsync(skuConfig);
            await _unitOfWork.SaveChangesAsync();

            return new SKUConfigViewModel
            {
                SKUConfigId = skuConfig.Id,
                Name = skuConfig.Name,
                Description = skuConfig.Description,
                Length = skuConfig.Length,
                Status = skuConfig.Status.ToString()
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }
    public async Task<ServiceResult<SKUConfigViewModel>> DeactivateSKUConfigAsync(int id)
    {
        try
        {
            var skuConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(id);
            if (skuConfig == null)
            {
                return Error.NotFound("SKUConfig not found");
            }

            if (skuConfig.Status == SKUConfigStatusEnum.Discontinued)
            {
                return Error.BadRequest("SKUConfig is already Discontinued");
            }

            skuConfig.Status = SKUConfigStatusEnum.Discontinued;
            await _unitOfWork.SKUConfigRepository.UpdateAsync(skuConfig);
            await _unitOfWork.SaveChangesAsync();

            return new SKUConfigViewModel
            {
                SKUConfigId = skuConfig.Id,
                Name = skuConfig.Name,
                Description = skuConfig.Description,
                Length = skuConfig.Length,
                Status = skuConfig.Status.ToString()
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }
}