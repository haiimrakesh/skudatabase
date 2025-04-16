using Microsoft.EntityFrameworkCore;
using SKUApp.Data.EFCore.InMemory;
using SKUApp.Domain.DataContracts;
using SKUApp.Domain.Entities;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;

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
    public async Task AddSKUConfig_ShouldAddValidSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var request = new CreateSKUConfigRequest
        {
            Name = "TestSKU",
            Description = "Test Description",
            Length = 10
        };

        // Act
        var result = await service.AddSKUConfigAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(await unitOfWork.SKUConfigRepository.GetByIdAsync(result.Value!.SKUConfigId));
        Assert.Equal("TESTSKU", result.Value.Name); // Name should be uppercase
    }

    [Fact]
    public async Task AddSKUConfig_ShouldReturnErrorIfNameIsEmpty()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var request = new CreateSKUConfigRequest
        {
            Name = "",
            Description = "Test Description",
            Length = 10
        };

        // Act
        var result = await service.AddSKUConfigAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(422, result.Error.ErrorCode);
        var vrMessage = result.Error.ValidationResults.FirstOrDefault(vr => vr.MemberNames.Contains("Name"));
        if (vrMessage == null)
        {
            Assert.NotNull(vrMessage);
            return;
        }
        Assert.Contains("Name", vrMessage.ErrorMessage);
    }

    [Fact]
    public async Task AddSKUConfig_ShouldReturnErrorIfLengthIsInvalid()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var request = new CreateSKUConfigRequest
        {
            Name = "TestSKU",
            Description = "Test Description",
            Length = 30 // Invalid length (greater than 25)
        };

        // Act
        var result = await service.AddSKUConfigAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(422, result.Error.ErrorCode);
        var vrMessage = result.Error.ValidationResults.FirstOrDefault(vr => vr.MemberNames.Contains("Length"));
        if (vrMessage == null)
        {
            Assert.NotNull(vrMessage);
            return;
        }
        Assert.Contains("Length", vrMessage.ErrorMessage);
    }

    [Fact]
    public async Task AddSKUConfig_ShouldReturnErrorIfDescriptionIsInvalid()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var request = new CreateSKUConfigRequest
        {
            Name = "TestSKU",
            Description = "A", // Invalid description (less than 3 characters)
            Length = 10
        };

        // Act
        var result = await service.AddSKUConfigAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(422, result.Error.ErrorCode);
        var vrMessage = result.Error.ValidationResults.FirstOrDefault(vr => vr.MemberNames.Contains("Description"));
        if (vrMessage == null)
        {
            Assert.NotNull(vrMessage);
            return;
        }
        Assert.Contains("Description", vrMessage.ErrorMessage);
    }

    [Fact]
    public async Task AddSKUConfig_ShouldReturnErrorIfNameIsDuplicate()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var existingSKUConfig = new SKUConfig
        {
            Name = "TESTSKU",
            Description = "Existing Description",
            Length = 10
        };
        await unitOfWork.SKUConfigRepository.AddAsync(existingSKUConfig);
        await unitOfWork.SaveChangesAsync();

        var request = new CreateSKUConfigRequest
        {
            Name = "TestSKU", // Duplicate name (case-insensitive)
            Description = "New Description",
            Length = 15
        };

        // Act
        var result = await service.AddSKUConfigAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Error.ErrorCode);
        Assert.Contains("already exists", result.Error.Message);
    }

    [Fact]
    public async Task AddSKUConfig_ShouldHandleExceptionGracefully()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var request = new CreateSKUConfigRequest
        {
            Name = "TestSKU",
            Description = "Test Description",
            Length = 10
        };

        // Simulate an exception by disposing the context
        unitOfWork.Dispose();

        // Act
        var result = await service.AddSKUConfigAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(500, result.Error.ErrorCode);
    }

    [Fact]
    public async Task GetSKUConfigById_ShouldReturnSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "Test", Length = 10, Description = "Test Description" };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.GetSKUConfigByIdAsync(skuConfig.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(skuConfig.Id, result.Value?.SKUConfigId);
    }

    [Fact]
    public async Task GetSKUConfigById_ShouldReturnErrorIfNotFound()
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

    [Fact]
    public async Task GetAllSKUConfigs_ShouldReturnAllConfigs()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        await unitOfWork.SKUConfigRepository.AddAsync(new SKUConfig { Name = "Test1", Description = "Desc1", Length = 10 });
        await unitOfWork.SKUConfigRepository.AddAsync(new SKUConfig { Name = "Test2", Description = "Desc2", Length = 15 });
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.GetAllSKUConfigsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value?.Count());
    }

    [Fact]
    public async Task GetAllSKUConfigs_ShouldReturnErrorIfNoneFound()
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
    public async Task UpdateSKUConfig_ShouldUpdateSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "TestSKU", Description = "Test Description", Length = 10 };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);
        var request = new UpdateSKUConfigRequest
        {
            Id = skuConfig.Id,
            Name = "UpdatedSKU",
            Description = "Updated Description",
            Length = 15
        };

        // Act
        var result = await service.UpdateSKUConfigAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("UpdatedSKU", result.Value?.Name);
    }

    [Fact]
    public async Task UpdateSKUConfig_ShouldReturnErrorIfNotFound()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var service = new SKUConfigService(unitOfWork);
        var request = new UpdateSKUConfigRequest
        {
            Id = 1,
            Name = "UpdatedSKU",
            Description = "Updated Description",
            Length = 15
        };

        // Act
        var result = await service.UpdateSKUConfigAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error.ErrorCode);
    }

    [Fact]
    public async Task DeleteSKUConfig_ShouldDeleteSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "TestSKU", Description = "Test Description", Length = 10 };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.DeleteSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task DeleteSKUConfig_ShouldReturnErrorIfNotFound()
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
        var skuConfig = new SKUConfig { Name = "TestSKU", Description = "Test Description", Length = 10, Status = SKUConfigStatusEnum.Draft };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var skuPart = new SKUPartConfig { Name = "TestPart", Length = 5 };
        await unitOfWork.SKUPartConfigRepository.AddAsync(skuPart);
        await unitOfWork.SaveChangesAsync();
        var skuConfigSequence = new SKUConfigSequence { SKUPartConfigId = skuPart.Id, Sequence = 1, SKUConfigId = skuConfig.Id };
        var skuConfigSequence2 = new SKUConfigSequence { SKUPartConfigId = skuPart.Id, Sequence = 1, SKUConfigId = skuConfig.Id };
        await unitOfWork.SKUConfigSequenceRepository.AddAsync(skuConfigSequence);
        await unitOfWork.SKUConfigSequenceRepository.AddAsync(skuConfigSequence2);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.ActivateSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(SKUConfigStatusEnum.Active.ToString(), result.Value?.Status);
    }

    [Fact]
    public async Task ActivateSKUConfig_ShouldReturnErrorIfNotFound()
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
    public async Task ActivateSKUConfig_ShouldReturnErrorIfAlreadyActive()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "Test", Length = 10, Status = SKUConfigStatusEnum.Active };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.ActivateSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Error.ErrorCode);
        Assert.Contains("already active", result.Error.Message);
    }

    [Fact]
    public async Task ActivateSKUConfig_ShouldReturnErrorIfDiscontinued()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "Test", Length = 10, Status = SKUConfigStatusEnum.Discontinued };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.ActivateSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Error.ErrorCode);
        Assert.Contains("discontinued status", result.Error.Message);
    }

    [Fact]
    public async Task ActivateSKUConfig_ShouldReturnErrorIfLengthMismatch()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "Test", Length = 10, Status = SKUConfigStatusEnum.Draft };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();

        // Simulate SKUConfig parts with a total length that does not match the SKUConfig length
        var skuPart1 = new SKUConfigSequence { SKUPartConfigId = 1, Sequence = 1, SKUConfigId = skuConfig.Id, SKUPartConfig = new SKUPartConfig { Length = 5 } };
        var skuPart2 = new SKUConfigSequence { SKUPartConfigId = 2, Sequence = 2, SKUConfigId = skuConfig.Id, SKUPartConfig = new SKUPartConfig { Length = 3 } };
        await unitOfWork.SKUConfigSequenceRepository.AddAsync(skuPart1);
        await unitOfWork.SKUConfigSequenceRepository.AddAsync(skuPart2);
        await unitOfWork.SaveChangesAsync();

        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.ActivateSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Error.ErrorCode);
        Assert.Contains("length does not match", result.Error.Message);
    }
    [Fact]
    public async Task DeactivateSKUConfig_ShouldDeactivateSKUConfig()
    {
        // Arrange
        var unitOfWork = GetInMemoryUnitOfWork();
        var skuConfig = new SKUConfig { Name = "TestSKU", Description = "Test Description", Length = 10, Status = SKUConfigStatusEnum.Active };
        await unitOfWork.SKUConfigRepository.AddAsync(skuConfig);
        await unitOfWork.SaveChangesAsync();
        var service = new SKUConfigService(unitOfWork);

        // Act
        var result = await service.DeactivateSKUConfigAsync(skuConfig.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(SKUConfigStatusEnum.Discontinued.ToString(), result.Value?.Status);
    }

    [Fact]
    public async Task DeactivateSKUConfig_ShouldReturnErrorIfNotFound()
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
}