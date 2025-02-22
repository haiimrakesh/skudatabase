namespace SKUApp.Domain.Services;

using System;
using System.Collections.Generic;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.UnitOfWork;

public class SKUPartConfigService : ISKUPartConfigService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUPartConfigService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddSKUPartConfig(SKUPartConfig sKUPartConfig)
    {
        // Cannot add if the SKUPartConfig is active
        // Check if the SKUPartConfig is active
        var sKUConfig = await _unitOfWork.SKUConfigRepository.GetByIdAsync(sKUPartConfig.SKUConfigId);
        // Perform a null check
        if (sKUConfig == null)
        {
            throw new InvalidOperationException("SKUConfig not found.");
        }

        if (sKUConfig.Status == SKUConfigStatusEnum.Active)
        {
            throw new InvalidOperationException("Cannot delete an active SKUConfig.");
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
    }

    public async Task DeleteSKUPartConfig(int id)
    {
        // Cannot delete if the SKUPartConfig is active
        // Check if the SKUPartConfig is active
        var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(id);
        // Perform a null check
        if (sKUPartConfig == null)
        {
            throw new InvalidOperationException("SKUPartValues not found.");
        }

        if (sKUPartConfig.Status == SKUConfigStatusEnum.Active)
        {
            throw new InvalidOperationException("Cannot delete an active SKUPartConfig.");
        }
        // Cannot delete a SKUPartConfig that has SKUPartValues
        // Cannot delete a SKUPartConfig that is part of a SKU
        // Check if the SKUPartConfig has any SKUPartValues
        var hasSKUPartValues = await _unitOfWork.SKUPartValuesRepository.FindAsync(v =>
        v.SKUPartConfigId == sKUPartConfig.Id);
        if (hasSKUPartValues.Any())
        {
            throw new InvalidOperationException("Cannot delete SKUPartConfig that has SKUPartValues.");
        }

        // Check if the SKUPartConfig is part of any SKU
        var isPartOfSKU = await _unitOfWork.SKUConfigSequenceRepository.FindAsync(s => s.SKUPartConfigId == id);
        if (isPartOfSKU.Any())
        {
            throw new InvalidOperationException("Cannot delete SKUPartConfig that is part of a SKU.");
        }

        // Remove the SKUPartConfig from the repository
        await _unitOfWork.SKUPartConfigRepository.DeleteAsync(sKUPartConfig);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddSKUPartValue(SKUPartValues sKUPartValues)
    {
        // Cannot add if the SKUPartConfig is active
        // Check if the SKUPartConfig is active
        var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(sKUPartValues.SKUPartConfigId);
        if (sKUPartConfig == null)
        {
            throw new InvalidOperationException("SKUPartConfig not found.");
        }

        if (sKUPartConfig.Status == SKUConfigStatusEnum.Active)
        {
            throw new InvalidOperationException("Cannot add to a active SKUPartConfig.");
        }

        string uniqueCode = sKUPartValues.UniqueCode;
        int skupartConfigId = sKUPartValues.SKUPartConfigId;
        // Check if the SKUPartValue exists by UniqueCode
        var exists = await _unitOfWork.SKUPartValuesRepository.GetSKUPartValuesByUniqueCode(uniqueCode, skupartConfigId);
        if (exists.Any())
        {
            throw new InvalidOperationException("SKUPartValue with the same UniqueCode already exists.");
        }

        // Add the SKUPartValues to the repository
        await _unitOfWork.SKUPartValuesRepository.AddAsync(sKUPartValues);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteSKUPartValue(int id)
    {
        // Get the SKUPartValues from the repository
        var sKUPartValues = await _unitOfWork.SKUPartValuesRepository.GetByIdAsync(id);
        // Perform a null check
        if (sKUPartValues == null)
        {
            throw new InvalidOperationException("SKUPartValues not found.");
        }
        // Cannot delete if the SKUPartConfig is active
        // Check if the SKUPartConfig is active
        var sKUPartConfig = await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(sKUPartValues.SKUPartConfigId);
        if (sKUPartConfig.Status == SKUConfigStatusEnum.Active)
        {
            throw new InvalidOperationException("Cannot delete an active SKUPartValue.");
        }

        // Remove the SKUPartValues from the repository
        await _unitOfWork.SKUPartValuesRepository.DeleteAsync(sKUPartValues);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<SKUPartConfig>> GetAllSKUPartConfigs()
    {
        return await _unitOfWork.SKUPartConfigRepository.GetAllAsync();
    }

    public async Task<SKUPartConfig> GetSKUPartConfigById(int id)
    {
        return await _unitOfWork.SKUPartConfigRepository.GetByIdAsync(id);
    }
}