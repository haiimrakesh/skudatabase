namespace SKUApp.Domain.Services;

using System;
using System.Collections.Generic;
using SKUApp.Domain.Entities;
using SKUApp.Common.ErrorHandling;
using SKUApp.Domain.DataContracts;
using SKUApp.Presentation.DataTransferObjects.ViewModels;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Common.Validation;
using SKUApp.Domain.ServiceContracts;

public class SKUPartConfigService : ISKUPartConfigService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUPartConfigService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ServiceResult<SKUPartConfigViewModel>> AddSKUPartConfigAsync(CreateSKUPartConfigRequest sKUPartConfigRequest)
    {
        try
        {
            Error validationError = Error.ValidationFailures();
            if (!ValidationHelper.Validate(sKUPartConfigRequest, validationError))
            {
                return validationError;
            }

            sKUPartConfigRequest.Name = sKUPartConfigRequest.Name.Trim().ToUpper();
            //Check if a sKUPartConfig with the same name exists
            var exists = await _unitOfWork.SKUPartConfigRepository.FindAsync(s => s.Name == sKUPartConfigRequest.Name);
            if (exists.Any())
            {
                return Error.BadRequest("SKUPartConfig with the same name already exists.");
            }
            var sKUPartConfig = new SKUPartConfig
            {
                Id = 0,
                Name = sKUPartConfigRequest.Name,
                Length = sKUPartConfigRequest.Length,
                GenericName = sKUPartConfigRequest.GenericName,
                Status = SKUConfigStatusEnum.Draft,
                Description = sKUPartConfigRequest.Description,
                IsAlphaNumeric = sKUPartConfigRequest.IsAlphaNumeric,
                IsCaseSensitive = sKUPartConfigRequest.IsCaseSensitive,
                IncludeSpacerAtTheEnd = sKUPartConfigRequest.IncludeSpacerAtTheEnd,
                AllowPreceedingZero = sKUPartConfigRequest.AllowPreceedingZero,
                RestrictConflictingLettersAndCharacters = sKUPartConfigRequest.RestrictConflictingLettersAndCharacters,
            };

            var defaultEntry = new SKUPartEntry
            {
                Id = 0,
                SKUPartConfigId = sKUPartConfig.Id,
                Name = sKUPartConfig.GenericName,
                UniqueCode = sKUPartConfig.GetDefaultGenericCode()
            };
            // Add the SKUPartConfig to the repository
            await _unitOfWork.SKUPartConfigRepository.AddAsync(sKUPartConfig);
            // Add the default Generic type to values. 
            await _unitOfWork.SKUPartEntryRepository.AddAsync(defaultEntry);
            await _unitOfWork.SaveChangesAsync();

            var vm = new SKUPartConfigViewModel
            {
                SKUPartConfigId = sKUPartConfig.Id,
                SKUPartConfigName = sKUPartConfig.Name,
                Length = sKUPartConfig.Length,
                GenericName = sKUPartConfig.GenericName,
                Status = sKUPartConfig.Status.ToString(),
                Description = sKUPartConfig.Description,
                IsAlphaNumeric = sKUPartConfig.IsAlphaNumeric,
                IsCaseSensitive = sKUPartConfig.IsCaseSensitive,
                IncludeSpacerAtTheEnd = sKUPartConfig.IncludeSpacerAtTheEnd,
                AllowPreceedingZero = sKUPartConfig.AllowPreceedingZero,
                RestrictConflictingLettersAndCharacters = sKUPartConfig.RestrictConflictingLettersAndCharacters
            };

            vm.SKUPartEntries.Add(new SKUPartEntryViewModel
            {
                SKUPartEntryId = defaultEntry.Id,
                SKUPartConfigId = defaultEntry.Id,
                Name = sKUPartConfig.GenericName,
                UniqueCode = defaultEntry.UniqueCode
            });

            return vm;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<ServiceResult<SKUPartConfigViewModel>> DeleteSKUPartConfigAsync(int id)
    {
        try
        {
            // Cannot delete if the SKUPartConfig is active
            // Check if the SKUPartConfig is active
            var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(id);
            // Perform a null check
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found.");
            }

            if (sKUPartConfig.Status == SKUConfigStatusEnum.Active)
            {
                return Error.BadRequest("Cannot delete an active SKUPartConfig.");
            }
            // Cannot delete a SKUPartConfig that has SKUPartValues
            // Cannot delete a SKUPartConfig that is part of a SKU
            // Check if the SKUPartConfig has any SKUPartValues
            var hasSKUPartValues = await _unitOfWork.SKUPartEntryRepository.FindAsync(v =>
            v.SKUPartConfigId == sKUPartConfig.Id);
            if (hasSKUPartValues.Any())
            {
                return Error.BadRequest("Cannot delete SKUPartConfig that has SKUPartValues.");
            }

            // Check if the SKUPartConfig is part of any SKU
            var isPartOfSKU = await _unitOfWork.SKUConfigSequenceRepository.FindAsync(s => s.SKUPartConfigId == id);
            if (isPartOfSKU.Any())
            {
                return Error.BadRequest("Cannot delete SKUPartConfig that is part of a SKU.");
            }

            // Remove the SKUPartConfig from the repository
            await _unitOfWork.SKUPartConfigRepository.DeleteAsync(sKUPartConfig);
            await _unitOfWork.SaveChangesAsync();

            return new SKUPartConfigViewModel
            {
                SKUPartConfigId = sKUPartConfig.Id,
                SKUPartConfigName = sKUPartConfig.Name,
                Length = sKUPartConfig.Length,
                GenericName = sKUPartConfig.GenericName,
                Status = sKUPartConfig.Status.ToString(),
                Description = sKUPartConfig.Description,
                IsAlphaNumeric = sKUPartConfig.IsAlphaNumeric,
                IsCaseSensitive = sKUPartConfig.IsCaseSensitive,
                IncludeSpacerAtTheEnd = sKUPartConfig.IncludeSpacerAtTheEnd,
                AllowPreceedingZero = sKUPartConfig.AllowPreceedingZero,
                RestrictConflictingLettersAndCharacters = sKUPartConfig.RestrictConflictingLettersAndCharacters
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<ServiceResult<IEnumerable<SKUPartConfigViewModel>>> GetAllSKUPartConfigsAsync()
    {
        try
        {
            IEnumerable<SKUPartConfig> list = await _unitOfWork.SKUPartConfigRepository.GetAllAsync();
            if (list == null || list.Count() == 0)
            {
                return Error.NotFound("No SKUPartConfigs found.");
            }
            return list.Select(pc => new SKUPartConfigViewModel
            {
                SKUPartConfigId = pc.Id,
                SKUPartConfigName = pc.Name,
                Length = pc.Length,
                GenericName = pc.GenericName,
                Status = pc.Status.ToString(),
                Description = pc.Description,
                IsAlphaNumeric = pc.IsAlphaNumeric,
                IsCaseSensitive = pc.IsCaseSensitive,
                IncludeSpacerAtTheEnd = pc.IncludeSpacerAtTheEnd,
                AllowPreceedingZero = pc.AllowPreceedingZero,
                RestrictConflictingLettersAndCharacters = pc.RestrictConflictingLettersAndCharacters
            }
            ).ToList();
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<ServiceResult<SKUPartConfigViewModel>> GetSKUPartConfigByIdAsync(int id)
    {
        try
        {
            SKUPartConfig? sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(id);
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found.");
            }
            return new SKUPartConfigViewModel
            {
                SKUPartConfigId = sKUPartConfig.Id,
                SKUPartConfigName = sKUPartConfig.Name,
                Length = sKUPartConfig.Length,
                GenericName = sKUPartConfig.GenericName,
                Status = sKUPartConfig.Status.ToString(),
                Description = sKUPartConfig.Description,
                IsAlphaNumeric = sKUPartConfig.IsAlphaNumeric,
                IsCaseSensitive = sKUPartConfig.IsCaseSensitive,
                IncludeSpacerAtTheEnd = sKUPartConfig.IncludeSpacerAtTheEnd,
                AllowPreceedingZero = sKUPartConfig.AllowPreceedingZero,
                RestrictConflictingLettersAndCharacters = sKUPartConfig.RestrictConflictingLettersAndCharacters
            };
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<ServiceResult<IEnumerable<SKUPartEntry>>> GetSKUPartEntriesByPartConfigIdAsync(int partConfigId)
    {
        try
        {
            IEnumerable<SKUPartEntry> list = await _unitOfWork.SKUPartEntryRepository.FindAsync(v => v.SKUPartConfigId == partConfigId);
            if (list == null || list.Count() == 0)
            {
                return Error.NotFound("No SKUPartEntries found.");
            }
            return list.ToList();
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public Task<ServiceResult<SKUPartConfigViewModel>> GetSKUPartConfigByIdAsync(int id, bool includeSKUPartEntries = false)
    {
        throw new NotImplementedException();
    }




}