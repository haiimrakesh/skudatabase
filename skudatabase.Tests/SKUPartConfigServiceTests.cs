using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skudatabase.domain.DataLayer;
using skudatabase.domain.Infrastructure;
using skudatabase.domain.Models;
using skudatabase.domain.Services;
using Xunit;

namespace skudatabase.Tests
{
    public class SKUPartServiceTests
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
        public async Task AddSKUPartConfig_ShouldAddSKUPartConfig()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            await unitOfWork.AddTestData_SKUConfig();
            await unitOfWork.SaveChangesAsync();
            var service = new SKUPartConfigService(unitOfWork);
            var sKUPartConfig = unitOfWork.GetTestData_SKUPartConfig();

            // Act
            await service.AddSKUPartConfig(sKUPartConfig);

            // Assert
            Assert.NotNull(await unitOfWork.SKUPartConfigRepository.GetByIdAsync(1));
            Assert.NotNull(await unitOfWork.SKUPartValuesRepository.FindAsync(sv => sv.UniqueCode == sKUPartConfig.GetDefaultGenericCode()));
        }

        [Fact]
        public async Task AddSKUPartConfig_ShouldThrowExceptionIfSKUConfigNotFound()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            var sKUPartConfig = unitOfWork.GetTestData_SKUPartConfig();
            var service = new SKUPartConfigService(unitOfWork);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddSKUPartConfig(sKUPartConfig));
        }

        [Fact]
        public async Task AddSKUPartConfig_ShouldThrowExceptionIfSKUConfigIsActive()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            await unitOfWork.AddTestData_SKUConfig("Test", SKUConfigStatusEnum.Active);
            await unitOfWork.SaveChangesAsync();
            var service = new SKUPartConfigService(unitOfWork);
            var sKUPartConfig = unitOfWork.GetTestData_SKUPartConfig();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddSKUPartConfig(sKUPartConfig));
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
            await service.DeleteSKUPartConfig(1);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => unitOfWork.SKUPartConfigRepository.GetByIdAsync(1));
        }

        [Fact]
        public async Task DeleteSKUPartConfig_ShouldThrowExceptionIfSKUPartConfigNotFound()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            var service = new SKUPartConfigService(unitOfWork);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteSKUPartConfig(1));
        }

        [Fact]
        public async Task DeleteSKUPartConfig_ShouldThrowExceptionIfSKUPartConfigIsActive()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            await unitOfWork.AddTestData_SKUConfig("Test", SKUConfigStatusEnum.Active);
            await unitOfWork.SaveChangesAsync();
            var service = new SKUPartConfigService(unitOfWork);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteSKUPartConfig(1));
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
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteSKUPartConfig(1));
        }

        [Fact]
        public async Task DeleteSKUPartConfig_ShouldThrowExceptionIfSKUPartConfigIsPartOfSKU()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            await unitOfWork.AddTestData_SKUConfig();
            await unitOfWork.AddTestData_SKUConfigSequence();
            await unitOfWork.SaveChangesAsync();
            var service = new SKUPartConfigService(unitOfWork);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteSKUPartConfig(1));
        }

        [Fact]
        public async Task AddSKUPartValue_ShouldAddSKUPartValue()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            await unitOfWork.AddTestData_SKUPartConfig();
            await unitOfWork.SaveChangesAsync();
            var sKUPartValues = new SKUPartValues
            {
                Id = 1,
                SKUPartConfigId = 1,
                Name = "TestValue",
                UniqueCode = "TestCode"
            };
            var service = new SKUPartConfigService(unitOfWork);

            // Act
            await service.AddSKUPartValue(sKUPartValues);

            // Assert
            Assert.NotNull(await unitOfWork.SKUPartValuesRepository.GetByIdAsync(1));
        }

        [Fact]
        public async Task AddSKUPartValue_ShouldThrowExceptionIfSKUPartConfigIsActive()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            await unitOfWork.AddTestData_SKUConfig("Test", SKUConfigStatusEnum.Active);
            await unitOfWork.SaveChangesAsync();
            var sKUPartValues = new SKUPartValues
            {
                Id = 1,
                SKUPartConfigId = 1,
                Name = "TestValue",
                UniqueCode = "TestCode"
            };
            var service = new SKUPartConfigService(unitOfWork);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddSKUPartValue(sKUPartValues));
        }

        [Fact]
        public async Task AddSKUPartValue_ShouldThrowExceptionIfSKUPartValueWithSameUniqueCodeExists()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            await unitOfWork.AddTestData_SKUConfig();
            await unitOfWork.AddTestData_SKUPartConfig();
            await unitOfWork.AddTestData_SKUPartValues("TestValue", "TestCode");
            await unitOfWork.SaveChangesAsync();
            var service = new SKUPartConfigService(unitOfWork);
            var newSKUPartValues = new SKUPartValues
            {
                Id = 2,
                SKUPartConfigId = 1,
                Name = "TestValue2",
                UniqueCode = "TestCode"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddSKUPartValue(newSKUPartValues));
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
            await service.DeleteSKUPartValue(1);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => unitOfWork.SKUPartValuesRepository.GetByIdAsync(1));
        }

        [Fact]
        public async Task DeleteSKUPartValue_ShouldThrowExceptionIfSKUPartValueNotFound()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            var service = new SKUPartConfigService(unitOfWork);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteSKUPartValue(1));
        }

        [Fact]
        public async Task DeleteSKUPartValue_ShouldThrowExceptionIfSKUPartConfigIsActive()
        {
            // Arrange
            var unitOfWork = GetInMemoryUnitOfWork();
            await unitOfWork.AddTestData_SKUPartConfig(SKUConfigStatusEnum.Active);
            await unitOfWork.AddTestData_SKUPartValues();
            await unitOfWork.AddTestData_SKUConfig("Test", SKUConfigStatusEnum.Active);
            await unitOfWork.SaveChangesAsync();
            var service = new SKUPartConfigService(unitOfWork);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteSKUPartValue(1));
        }
    }
}