using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.EntityFramework.InMemory;
using SKUApp.Domain.Infrastructure.UnitOfWork;
using SKUApp.Domain.Services;
using Xunit;

namespace SKUApp.Domain.Services.Tests;

public class SKUConfigServiceTests
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
    public async Task AddSKUConfig_ShouldAddSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var skuConfig = new SKUConfig { Name = "Test", Length = 10 };

        // Act
        var result = await service.AddSKUConfigAsync(skuConfig);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(await unitOfWork.SKUConfigRepository.GetByIdAsync(skuConfig.Id));
    }

    [Fact]
    public async Task AddSKUConfig_ShouldReturnErrorIfLengthInvalid()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var skuConfig = new SKUConfig { Name = "Test", Length = 30 };

        // Act
        var result = await service.AddSKUConfigAsync(skuConfig);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Error.ErrorCode);
    }

    [Fact]
    public async Task AddSKUConfig_ShouldReturnErrorIfNameIsEmpty()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var skuConfig = new SKUConfig { Name = "", Length = 10 };

        // Act
        var result = await service.AddSKUConfigAsync(skuConfig);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Error.ErrorCode);
    }

    [Fact]
    public async Task DeleteSKUConfig_ShouldDeleteSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "Test", Length = 10 };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.DeleteSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task DeleteSKUConfig_ShouldReturnErrorIfSKUConfigNotFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.DeleteSKUConfigAsync(1);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }

    [Fact]
    public async Task ActivateSKUConfig_ShouldActivateSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "Test", Length = 10, Status = SKUConfigStatusEnum.Draft };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.ActivateSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(SKUConfigStatusEnum.Active, result?.Value?.Status);
    }

    [Fact]
    public async Task ActivateSKUConfig_ShouldReturnErrorIfSKUConfigNotFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.ActivateSKUConfigAsync(1);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }

    [Fact]
    public async Task DeactivateSKUConfig_ShouldDeactivateSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "Test", Length = 10, Status = SKUConfigStatusEnum.Active };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.DeactivateSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(SKUConfigStatusEnum.Discontinued, result.Value?.Status);
    }

    [Fact]
    public async Task DeactivateSKUConfig_ShouldReturnErrorIfSKUConfigNotFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.DeactivateSKUConfigAsync(1);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }

    [Fact]
    public async Task GetAllSKUConfigs_ShouldReturnAllSKUConfigs()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.SKUConfigRepository.AddAsync(new SKUConfig { Name = "Test1", Length = 10 });
        await unitOfWork.SKUConfigRepository.AddAsync(new SKUConfig { Name = "Test2", Length = 15 });
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.GetAllSKUConfigsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value?.Count());
    }

    [Fact]
    public async Task GetAllSKUConfigs_ShouldReturnErrorIfNoSKUConfigsFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.GetAllSKUConfigsAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }

    [Fact]
    public async Task GetSKUConfigById_ShouldReturnSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "Test", Length = 10 };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.GetSKUConfigByIdAsync(skuConfig.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(skuConfig.Id, result.Value?.Id);
    }

    [Fact]
    public async Task GetSKUConfigById_ShouldReturnErrorIfSKUConfigNotFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.GetSKUConfigByIdAsync(1);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }
}