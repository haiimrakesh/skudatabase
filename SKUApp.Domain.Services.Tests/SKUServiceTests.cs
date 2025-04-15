/*using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Services;
using Xunit;

namespace SKUApp.Domain.Services.Tests;

public class SKUServiceTests
{
    private ISKUUnitOfWork GetInMemoryUnitOfWork()
    {
        var options = new DbContextOptionsBuilder<InMemoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new InMemoryDbContext(options);
        return new InMemorySKUUnitOfWork(context);
    }

    [Fact]
    public async Task AddSKU_ShouldAddSKU()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUService(unitOfWork);
        var sku = new SKU { SKUCode = "TestSKU" };

        // Act
        var result = await service.AddSKUAsync(sku);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(await unitOfWork.SKURepository.GetByIdAsync(sku.Id));
    }

    [Fact]
    public async Task DeleteSKU_ShouldDeleteSKU()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var sku = new SKU { SKUCode = "TestSKU" };
        await unitOfWork.SKURepository.AddAsync(sku);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUService(unitOfWork);

        // Act
        var result = await service.DeleteSKUAsync(sku.Id);
        await unitOfWork.SaveChangesAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(await unitOfWork.SKURepository.GetByIdAsync(sku.Id));
    }

    [Fact]
    public async Task DeleteSKU_ShouldReturnErrorIfSKUNotFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUService(unitOfWork);

        // Act
        var result = await service.DeleteSKUAsync(1);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }

    [Fact]
    public async Task GetAllSKUs_ShouldReturnAllSKUs()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.SKURepository.AddAsync(new SKU { SKUCode = "TestSKU1" });
        await unitOfWork.SKURepository.AddAsync(new SKU { SKUCode = "TestSKU2" });
        await unitOfWork.SaveChangesAsync();
        var service = new SKUService(unitOfWork);

        // Act
        var result = await service.GetAllSKUsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value?.Count());
    }

    [Fact]
    public async Task GetAllSKUs_ShouldReturnErrorIfNoSKUsFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUService(unitOfWork);

        // Act
        var result = await service.GetAllSKUsAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }

    [Fact]
    public async Task GetSKUById_ShouldReturnSKU()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var sku = new SKU { SKUCode = "TestSKU" };
        await unitOfWork.SKURepository.AddAsync(sku);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUService(unitOfWork);

        // Act
        var result = await service.GetSKUByIdAsync(sku.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(sku.Id, result.Value?.Id);
    }

    [Fact]
    public async Task GetSKUById_ShouldReturnErrorIfSKUNotFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUService(unitOfWork);

        // Act
        var result = await service.GetSKUByIdAsync(1);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }
}*/