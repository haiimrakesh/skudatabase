using SKUApp.Common.ErrorHandling;
using SKUApp.Domain.DataContracts;
using SKUApp.Domain.Infrastructure.Services;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Presentation.DataTransferObjects.ViewModels;
using SKUApp.Domain.Entities;
using SKUApp.Common.Validation;
using SKUApp.Domain.Services;

public class SKUConfigSequenceService : ISKUConfigSequenceService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUConfigSequenceService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<SKUConfigSequenceViewModel>> AddSKUSequenceAsync(CreateSKUConfigSequenceRequest skuSequenceRequest)
    {
        try
        {
            Error validationError = Error.ValidationFailures();
            if (!ValidationHelper.Validate(skuSequenceRequest, validationError))
            {
                return validationError;
            }

            var skuConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(skuSequenceRequest.SKUConfigId);
            if (skuConfig == null)
            {
                return Error.NotFound("SKUConfig not found");
            }
            if (skuConfig.Status != SKUConfigStatusEnum.Draft)
            {
                return Error.BadRequest("SKUConfig must be in draft state to add SKUConfigSequence");
            }

            var skuPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(skuSequenceRequest.SKUPartConfigId);
            if (skuPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found");
            }
            if (skuPartConfig.Status != SKUConfigStatusEnum.Active)
            {
                return Error.BadRequest("SKUPartConfig must be in active state to be added to SKUConfig");
            }
            var skuConfigSequence = new SKUConfigSequence
            {
                Id = 0,
                SKUConfigId = skuConfig.Id,
                SKUPartConfigId = skuPartConfig.Id,
                Sequence = skuSequenceRequest.Sequence,
                RelationshipDescription = skuSequenceRequest.RelationshipDescription,
            };
            await _unitOfWork.SKUConfigSequenceRepository.AddAsync(skuConfigSequence);
            await _unitOfWork.SaveChangesAsync();

            return new SKUConfigSequenceViewModel
            {
                SKUConfigSequenceId = skuConfigSequence.Id,
                SKUConfigId = skuConfigSequence.SKUConfigId,
                SKUPartConfigId = skuConfigSequence.SKUPartConfigId,
                Sequence = skuConfigSequence.Sequence,
                RelationshipDescription = skuConfigSequence.RelationshipDescription,
                SKUPartName = skuPartConfig.Name,
                SKUPartLength = skuPartConfig.Length
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUConfigSequenceViewModel>> DeleteSKUSequenceAsync(int skuSequenceId)
    {
        try
        {
            var skuConfigSequence = await _unitOfWork.SKUConfigSequenceRepository.GetByIdAsync(skuSequenceId);
            if (skuConfigSequence == null)
            {
                return Error.NotFound("SKUConfigSequence not found");
            }
            var skuConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(skuConfigSequence.SKUConfigId);
            if (skuConfig == null)
            {
                return Error.NotFound("SKUConfig not found");
            }
            if (skuConfig.Status != SKUConfigStatusEnum.Draft)
            {
                return Error.BadRequest("SKUConfig must be in draft state make edits");
            }

            await _unitOfWork.SKUConfigSequenceRepository.DeleteAsync(skuConfigSequence);
            await _unitOfWork.SaveChangesAsync();

            return new SKUConfigSequenceViewModel
            {
                SKUConfigSequenceId = skuConfigSequence.Id,
                SKUConfigId = skuConfigSequence.SKUConfigId,
                SKUPartConfigId = skuConfigSequence.SKUPartConfigId,
                Sequence = skuConfigSequence.Sequence,
                RelationshipDescription = skuConfigSequence.RelationshipDescription
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUConfigViewModel>> ReorderSKUSequenceAsync(ReOrderSKUConfigSequenceRequest reOrderSKUConfigSequenceRequest)
    {
        try
        {
            Error validationError = Error.ValidationFailures();
            if (!ValidationHelper.Validate(reOrderSKUConfigSequenceRequest, validationError))
            {
                return validationError;
            }

            var skuConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(reOrderSKUConfigSequenceRequest.SKUConfigId);
            if (skuConfig == null)
            {
                return Error.NotFound("SKUConfig not found");
            }
            if (skuConfig.Status != SKUConfigStatusEnum.Draft)
            {
                return Error.BadRequest("SKUConfig must be in draft state to be able to change the order");
            }

            var skuConfigSequences = await _unitOfWork.SKUConfigSequenceRepository.GetSKUConfigSequenceByConfigIdAsync(reOrderSKUConfigSequenceRequest.SKUConfigId);
            if (skuConfigSequences.Count() != reOrderSKUConfigSequenceRequest.SKUConfigSequenceIds.Count())
            {
                return Error.BadRequest("SKUConfigSequenceIds count does not match SKUConfigSequence count");
            }

            int order = 1;
            foreach (int id in reOrderSKUConfigSequenceRequest.SKUConfigSequenceIds)
            {
                var skuConfigSequence = skuConfigSequences.FirstOrDefault(s => s.Id == id);
                if (skuConfigSequence == null)
                {
                    return Error.NotFound($"SKUConfigSequence with ID {id} not found");
                }
                skuConfigSequence.Sequence = order++;
                await _unitOfWork.SKUConfigSequenceRepository.UpdateAsync(skuConfigSequence);
            }

            await _unitOfWork.SaveChangesAsync();

            return await new SKUConfigService(_unitOfWork)
            .GetSKUConfigByIdAsync(reOrderSKUConfigSequenceRequest.SKUConfigId);
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUConfigSequenceViewModel>> UpdateSKUSequenceAsync(UpdateSKUConfigSequenceRequest skuSequenceRequest)
    {
        try
        {
            var skuConfigSequence = await _unitOfWork.SKUConfigSequenceRepository.GetByIdAsync(skuSequenceRequest.SKUConfigSequenceId);
            if (skuConfigSequence == null)
            {
                return Error.NotFound("SKUConfigSequence not found");
            }

            if (skuConfigSequence.RelationshipDescription !=
            skuSequenceRequest.RelationshipDescription)
            {
                skuConfigSequence.RelationshipDescription = skuSequenceRequest.RelationshipDescription;
            }
            else
            {
                return Error.BadRequest("No changes detected");
            }

            await _unitOfWork.SKUConfigSequenceRepository.UpdateAsync(skuConfigSequence);
            await _unitOfWork.SaveChangesAsync();

            return new SKUConfigSequenceViewModel
            {
                SKUConfigSequenceId = skuConfigSequence.Id,
                SKUConfigId = skuConfigSequence.SKUConfigId,
                SKUPartConfigId = skuConfigSequence.SKUPartConfigId,
                Sequence = skuConfigSequence.Sequence,
                RelationshipDescription = skuConfigSequence.RelationshipDescription
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }
}