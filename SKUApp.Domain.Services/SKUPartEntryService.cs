using SKUApp.Common.ErrorHandling;
using SKUApp.Common.Validation;
using SKUApp.Domain.DataContracts;
using SKUApp.Domain.Entities;
using SKUApp.Domain.ServiceContracts;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Presentation.DataTransferObjects.ViewModels;

public class SKUPartEntryService : ISKUPartEntryService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUPartEntryService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<SKUPartEntryViewModel>> AddSKUPartEntryAsync(CreateSKUPartEntryRequest sKUPartEntryRequest)
    {
        try
        {
            Error validationError = Error.ValidationFailures();
            if (!ValidationHelper.Validate(sKUPartEntryRequest, validationError))
            {
                return validationError;
            }

            // Cannot add if the SKUPartConfig is active
            // Check if the SKUPartConfig is active
            var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(sKUPartEntryRequest.SKUPartConfigId);
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found.");
            }

            if (sKUPartConfig.Status != SKUConfigStatusEnum.Discontinued)
            {
                return Error.BadRequest("Cannot add to a Discontinued SKUPartConfig.");
            }

            string uniqueCode = sKUPartEntryRequest.UniqueCode.Trim().ToUpper();
            if (uniqueCode.Length != sKUPartConfig.Length)
            {
                return Error.BadRequest($"UniqueCode length {uniqueCode.Length} does not match the SKUPartConfig length {sKUPartConfig.Length}.");
            }
            int skupartConfigId = sKUPartEntryRequest.SKUPartConfigId;
            // Check if the SKUPartValue exists by UniqueCode
            var exists = await _unitOfWork.SKUPartEntryRepository.GetSKUPartEntriesByUniqueCode(uniqueCode, skupartConfigId);
            if (exists.Any())
            {
                return Error.BadRequest("SKUPartValue with the same UniqueCode already exists.");
            }

            var sKUPartEntry = new SKUPartEntry
            {
                Id = 0,
                SKUPartConfigId = sKUPartEntryRequest.SKUPartConfigId,
                Name = sKUPartEntryRequest.Name.Trim().ToUpper(),
                UniqueCode = uniqueCode
            };

            await _unitOfWork.SKUPartEntryRepository.AddAsync(sKUPartEntry);
            await _unitOfWork.SaveChangesAsync();

            return new SKUPartEntryViewModel
            {
                SKUPartEntryId = sKUPartEntry.Id,
                SKUPartConfigId = sKUPartEntry.SKUPartConfigId,
                Name = sKUPartEntry.Name,
                UniqueCode = sKUPartEntry.UniqueCode
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUPartEntryViewModel>> DeleteSKUPartEntryAsync(int partEntryId)
    {
        try
        {
            var sKUPartEntry = await _unitOfWork.SKUPartEntryRepository.GetByIdAsync(partEntryId);
            if (sKUPartEntry == null)
            {
                return Error.NotFound("sKUPartEntry not found.");
            }
            var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(sKUPartEntry.SKUPartConfigId);
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found.");
            }

            if (sKUPartConfig.Status != SKUConfigStatusEnum.Discontinued)
            {
                return Error.BadRequest("Cannot modify to a discontinued SKUPartConfig.");
            }

            await _unitOfWork.SKUPartEntryRepository.DeleteAsync(sKUPartEntry);
            await _unitOfWork.SaveChangesAsync();

            return new SKUPartEntryViewModel
            {
                SKUPartEntryId = sKUPartEntry.Id,
                SKUPartConfigId = sKUPartEntry.SKUPartConfigId,
                Name = sKUPartEntry.Name,
                UniqueCode = sKUPartEntry.UniqueCode
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUPartEntryViewModel>> UpdateSKUPartEntryAsync(UpdateSKUPartEntryRequest sKUPartEntryUpdateRequest)
    {
        try
        {
            Error validationError = Error.ValidationFailures();
            if (!ValidationHelper.Validate(sKUPartEntryUpdateRequest, validationError))
            {
                return validationError;
            }

            var sKUPartEntry = await _unitOfWork.SKUPartEntryRepository.GetByIdAsync(sKUPartEntryUpdateRequest.Id);
            if (sKUPartEntry == null)
            {
                return Error.NotFound("sKUPartEntry not found.");
            }
            var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(sKUPartEntry.SKUPartConfigId);
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found.");
            }

            if (sKUPartConfig.Status != SKUConfigStatusEnum.Discontinued)
            {
                return Error.BadRequest("Cannot modify to a discontinued SKUPartConfig.");
            }

            sKUPartEntry.Name = sKUPartEntryUpdateRequest.Name.Trim().ToUpper();

            await _unitOfWork.SKUPartEntryRepository.UpdateAsync(sKUPartEntry);
            await _unitOfWork.SaveChangesAsync();

            return new SKUPartEntryViewModel
            {
                SKUPartEntryId = sKUPartEntry.Id,
                SKUPartConfigId = sKUPartEntry.SKUPartConfigId,
                Name = sKUPartEntry.Name,
                UniqueCode = sKUPartEntry.UniqueCode
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }
}