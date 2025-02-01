namespace skudatabase.domain.Services;

using System;
using skudatabase.domain.Infrastructure;
using skudatabase.domain.Models;

public class SKUPartConfigService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUPartConfigService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddSKUPartConfig(SKUPartConfig sKUPartConfig)
    {
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
}