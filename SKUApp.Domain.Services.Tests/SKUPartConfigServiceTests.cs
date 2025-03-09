
using Microsoft.EntityFrameworkCore;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.EntityFramework.InMemory;
using SKUApp.Domain.Infrastructure.UnitOfWork;
using SKUApp.Domain.Services;

namespace SKUApp.Domain.Services.Tests;

public class SKUPartConfigServiceTests
{
    private ISKUUnitOfWork GetInMemoryUnitOfWork()
    {
        var options = new DbContextOptionsBuilder<InMemoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new InMemoryDbContext(options);
        return new InMemorySKUUnitOfWork(context);
    }

    public static ISKUUnitOfWork GetInMemoryUnitOfWorkStatic()
    {
        var options = new DbContextOptionsBuilder<InMemoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new InMemoryDbContext(options);
        return new InMemorySKUUnitOfWork(context);
    }

    [Fact]
    public async Task AddSKUPartConfig_ShouldAddSKUPartConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUPartConfigService(unitOfWork);
        var sKUPartConfig = unitOfWork.GetTestData_SKUPartConfig();

        // Act
        await service.AddSKUPartConfigAsync(sKUPartConfig);

        // Assert
        //Should have created the SKUPartConfig with Id 1
        Assert.NotNull(await unitOfWork.SKUPartConfigRepository.GetByIdAsync(1));
        //Should have created the default SKUPartValues with UniqueCode as default generic code
        Assert.NotNull(await unitOfWork.SKUPartEntryRepository.FindAsync(sv => sv.UniqueCode == sKUPartConfig.GetDefaultGenericCode()));
    }

    [Fact]
    public async Task AddSKUPartConfig_ShouldThrowErrorIfPartConfigAlreadyExists()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.AddTestData_SKUPartConfig();
        await unitOfWork.SaveChangesAsync();
        var service = new SKUPartConfigService(unitOfWork);
        var sKUPartConfig = unitOfWork.GetTestData_SKUPartConfig();

        // Act
        var result = await service.AddSKUPartConfigAsync(sKUPartConfig);

        // Assert
        //should throw BadRequest error
        Assert.Equal(400, result.Error.ErrorCode);
    }

    [Fact]
    public async Task DeleteSKUPartConfig_ShouldDeleteSKUPartConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var sKUPartConfig = unitOfWork.GetTestData_SKUPartConfig();
        await unitOfWork.SKUPartConfigRepository.AddAsync(sKUPartConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUPartConfigService(unitOfWork);

        // Act
        await service.DeleteSKUPartConfigAsync(1);

        // Assert
        Assert.Null(await unitOfWork.SKUPartConfigRepository.GetByIdAsync(1));
    }

    [Fact]
    public async Task DeleteSKUPartConfig_ShouldThrowExceptionIfSKUPartConfigHasSKUPartValues()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.AddTestData_SKUPartConfig();
        await unitOfWork.AddTestData_SKUPartValues();
        await unitOfWork.SaveChangesAsync();
        var service = new SKUPartConfigService(unitOfWork);

        // Act & Assert
        var result = await service.DeleteSKUPartConfigAsync(1);
        Assert.Equal(400, result.Error.ErrorCode);
    }

    [Fact]
    public async Task DeleteSKUPartConfig_ShouldThrowExceptionIfSKUPartConfigIsPartOfSKU()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.AddTestData_SKUPartConfig();
        await unitOfWork.AddTestData_SKUConfigSequence();
        await unitOfWork.SaveChangesAsync();
        var service = new SKUPartConfigService(unitOfWork);

        // Act & Assert
        var result = await service.DeleteSKUPartConfigAsync(1);
        Assert.Equal(400, result.Error.ErrorCode);
    }

    [Fact]
    public async Task AddSKUPartValue_ShouldAddSKUPartValue()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.AddTestData_SKUPartConfig();
        await unitOfWork.SaveChangesAsync();
        var sKUPartValues = new SKUPartEntry
        {
            Id = 1,
            SKUPartConfigId = 1,
            Name = "TestValue",
            UniqueCode = "TestCode"
        };
        var service = new SKUPartConfigService(unitOfWork);

        // Act
        await service.AddSKUPartEntryAsync(sKUPartValues);

        // Assert
        Assert.NotNull(await unitOfWork.SKUPartEntryRepository.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddSKUPartValue_ShouldThrowExceptionIfSKUPartConfigIsActive()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.AddTestData_SKUPartConfig(SKUConfigStatusEnum.Active);
        await unitOfWork.SaveChangesAsync();
        var sKUPartValues = new SKUPartEntry
        {
            Id = 1,
            SKUPartConfigId = 1,
            Name = "TestValue",
            UniqueCode = "TestCode"
        };
        var service = new SKUPartConfigService(unitOfWork);

        // Act & Assert
        var result = await service.AddSKUPartEntryAsync(sKUPartValues);
        Assert.Equal(400, result.Error.ErrorCode);
    }

    [Fact]
    public async Task AddSKUPartValue_ShouldThrowExceptionIfSKUPartValueWithSameUniqueCodeExists()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.AddTestData_SKUPartConfig();
        await unitOfWork.AddTestData_SKUPartValues("TestValue", "TestCode");
        await unitOfWork.SaveChangesAsync();
        var service = new SKUPartConfigService(unitOfWork);
        var newSKUPartValues = new SKUPartEntry
        {
            Id = 2,
            SKUPartConfigId = 1,
            Name = "TestValue2",
            UniqueCode = "TestCode"
        };

        // Act & Assert
        var result = await service.AddSKUPartEntryAsync(newSKUPartValues);
        Assert.Equal(400, result.Error.ErrorCode);
    }

    [Fact]
    public async Task DeleteSKUPartValue_ShouldDeleteSKUPartValue()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.AddTestData_SKUPartConfig();
        await unitOfWork.AddTestData_SKUPartValues();
        await unitOfWork.SaveChangesAsync();
        var service = new SKUPartConfigService(unitOfWork);

        // Act
        await service.DeleteSKUPartEntryAsync(1);

        // Assert
        Assert.Null(await unitOfWork.SKUPartEntryRepository.GetByIdAsync(1));
    }

    [Fact]
    public async Task DeleteSKUPartValue_ShouldThrowExceptionIfSKUPartValueNotFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUPartConfigService(unitOfWork);

        // Act & Assert
        var result = await service.DeleteSKUPartEntryAsync(1);
        Assert.Equal(404, result.Error.ErrorCode);
    }

    [Fact]
    public async Task DeleteSKUPartValue_ShouldThrowExceptionIfSKUPartConfigIsActive()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.AddTestData_SKUPartConfig(SKUConfigStatusEnum.Active);
        await unitOfWork.AddTestData_SKUPartValues();
        await unitOfWork.SaveChangesAsync();
        var service = new SKUPartConfigService(unitOfWork);

        // Act & Assert
        var result = await service.DeleteSKUPartEntryAsync(1);
        Assert.Equal(400, result.Error.ErrorCode);
    }
}
