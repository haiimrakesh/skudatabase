namespace skudatabase.Tests;

using System;
using skudatabase.domain.Models;
using Microsoft.EntityFrameworkCore;
using skudatabase.domain.Services;
using skudatabase.domain.Infrastructure;
using skudatabase.domain.DataLayer;

public class SKUPartConfigServiceTests
{
    [Fact]
    public async void AddSKUPartConfig_ShouldAddSKUConfig()
    {
        // Arrange
        ISKUUnitOfWork unitOfWork = new InMemorySKUUnitOfWork(new InMemoryDbContext(new DbContextOptions<InMemoryDbContext>()));
        var sKUPartConfig = new SKUPartConfig
        {
            Id = 1,
            Name = "Test",
            Length = 3,
            GenericName = "Test",
            IsAlphaNumeric = true,
            IsCaseSensitive = true
        };
        var service = new SKUPartConfigService(unitOfWork);

        // Act
        await service.AddSKUPartConfig(sKUPartConfig);

        string defaultGenericCode = sKUPartConfig.GetDefaultGenericCode();
        // Assert
        Assert.NotNull(await unitOfWork.SKUPartConfigRepository.GetByIdAsync(1));
        Assert.NotNull(await unitOfWork.SKUPartValuesRepository.FindAsync(sv => sv.UniqueCode == defaultGenericCode));
    }
}