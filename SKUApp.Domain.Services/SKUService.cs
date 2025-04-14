using SKUApp.Domain.Entities;
using SKUApp.Common.ErrorHandling;
using SKUApp.Domain.Infrastructure.Services;
using SKUApp.Domain.DataContracts;
using SKUApp.Presentation.DataTransferObjects.ViewModels;

namespace SKUApp.Domain.Services;

public class SKUService : ISKUService
{
    private readonly ISKUUnitOfWork _unitOfWork;
    public SKUService(ISKUUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SKU>> AddSKUAsync(SKU sku)
    {
        try
        {
            await _unitOfWork.SKURepository.AddAsync(sku);
            return sku;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public Task<Result<SKUViewModel>> AddSKUAsync(CreateSKURequest skuConfig)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<SKU>> DeleteSKUAsync(int id)
    {
        try
        {
            var SKU = await _unitOfWork.SKURepository.GetByIdAsync(id);
            if (SKU == null)
            {
                return Error.NotFound("SKU not found.");
            }

            await _unitOfWork.SKURepository.DeleteAsync(SKU);
            return SKU;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<SKU>>> GetAllSKUsAsync()
    {
        try
        {
            IEnumerable<SKU> sKUs = await _unitOfWork.SKURepository.GetAllAsync();
            if (sKUs == null || sKUs.Count() == 0)
            {
                return Error.NotFound("SKUs not found.");
            }
            return sKUs.ToList();
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public async Task<Result<SKU>> GetSKUByIdAsync(int id)
    {
        try
        {
            var sKU = await _unitOfWork.SKURepository.GetByIdAsync(id);
            if (sKU == null)
            {
                return Error.NotFound("SKU not found.");
            }
            return sKU;
        }
        catch (Exception ex)
        {
            return Error.InternalServerError(ex.Message);
        }
    }

    public Task<Result<SKUViewModel>> GetSKUByIdAsync(int id, bool includeSKUPartConfig = false)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SKUViewModel>> UpdateSKUAsync(UpdateSKURequest skuConfig)
    {
        throw new NotImplementedException();
    }

    Task<Result<SKUViewModel>> ISKUService.DeleteSKUAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<Result<IEnumerable<SKUViewModel>>> ISKUService.GetAllSKUsAsync()
    {
        throw new NotImplementedException();
    }
}