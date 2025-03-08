namespace SKUApp.Domain.Services;

using System;
using System.Collections.Generic;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.ErrorHandling;
using SKUApp.Domain.Infrastructure.Services;
using SKUApp.Domain.Infrastructure.UnitOfWork;

public class SKUPartConfigService : ISKUPartConfigService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUPartConfigService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SKUPartConfig>> AddSKUPartConfigAsync(SKUPartConfig sKUPartConfig)
    {
        try
        {
            sKUPartConfig.Id = 0;
            sKUPartConfig.Name = sKUPartConfig.Name.Trim().ToUpper();
            //Check if a sKUPartConfig with the same name exists
            var exists = await _unitOfWork.SKUPartConfigRepository.FindAsync(s => s.Name == sKUPartConfig.Name);
            if (exists.Any())
            {
                return Error.BadRequest("SKUPartConfig with the same name already exists.");
            }
            // Add the SKUPartConfig to the repository
            await _unitOfWork.SKUPartConfigRepository.AddAsync(sKUPartConfig);
            // Add the default Generic type to values. 
            await _unitOfWork.SKUPartValuesRepository.AddAsync(
                new SKUPartValues
                {
                    SKUPartConfigId = sKUPartConfig.Id,
                    Name = sKUPartConfig.GenericName,
                    UniqueCode = sKUPartConfig.GetDefaultGenericCode()
                });
            await _unitOfWork.SaveChangesAsync();

            return sKUPartConfig;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUPartConfig>> DeleteSKUPartConfigAsync(int id)
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
            var hasSKUPartValues = await _unitOfWork.SKUPartValuesRepository.FindAsync(v =>
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

            return sKUPartConfig;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUPartValues>> AddSKUPartValueAsync(SKUPartValues sKUPartValues)
    {
        try
        {
            // Cannot add if the SKUPartConfig is active
            // Check if the SKUPartConfig is active
            var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(sKUPartValues.SKUPartConfigId);
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found.");
            }

            if (sKUPartConfig.Status == SKUConfigStatusEnum.Active)
            {
                return Error.BadRequest("Cannot add to a active SKUPartConfig.");
            }

            string uniqueCode = sKUPartValues.UniqueCode;
            int skupartConfigId = sKUPartValues.SKUPartConfigId;
            // Check if the SKUPartValue exists by UniqueCode
            var exists = await _unitOfWork.SKUPartValuesRepository.GetSKUPartValuesByUniqueCode(uniqueCode, skupartConfigId);
            if (exists.Any())
            {
                return Error.BadRequest("SKUPartValue with the same UniqueCode already exists.");
            }

            // Add the SKUPartValues to the repository
            await _unitOfWork.SKUPartValuesRepository.AddAsync(sKUPartValues);
            await _unitOfWork.SaveChangesAsync();

            return sKUPartValues;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUPartValues>> DeleteSKUPartValueAsync(int id)
    {
        try
        {
            // Get the SKUPartValues from the repository
            var sKUPartValues = await _unitOfWork.SKUPartValuesRepository.GetByIdAsync(id);
            // Perform a null check
            if (sKUPartValues == null)
            {
                return Error.NotFound("SKUPartValues not found.");
            }
            // Cannot delete if the SKUPartConfig is active
            // Check if the SKUPartConfig is active
            var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(sKUPartValues.SKUPartConfigId);
            // Perform a null check
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found.");
            }
            if (sKUPartConfig.Status == SKUConfigStatusEnum.Active)
            {
                return Error.BadRequest("Cannot delete an active SKUPartConfig.");
            }

            // Remove the SKUPartValues from the repository
            await _unitOfWork.SKUPartValuesRepository.DeleteAsync(sKUPartValues);
            await _unitOfWork.SaveChangesAsync();

            return sKUPartValues;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<SKUPartConfig>>> GetAllSKUPartConfigsAsync()
    {
        try
        {
            IEnumerable<SKUPartConfig> list = await _unitOfWork.SKUPartConfigRepository.GetAllAsync();
            if (list == null || list.Count() == 0)
            {
                return Error.NotFound("No SKUPartConfigs found.");
            }
            return list.ToList();
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKUPartConfig>> GetSKUPartConfigByIdAsync(int id)
    {
        try
        {
            SKUPartConfig? sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(id);
            if (sKUPartConfig == null)
            {
                return Error.NotFound("SKUPartConfig not found.");
            }
            return sKUPartConfig;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }
}