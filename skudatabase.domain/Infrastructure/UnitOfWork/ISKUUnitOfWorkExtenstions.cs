using System.Net;
using skudatabase.domain.Infrastructure;
using skudatabase.domain.Models;

namespace skudatabase.domain.Infrastructure.UnitOfWork;

public static class ISKUUnitOfWorkExtenstions
{
    public static async Task AddTestData_SKUPartConfig(this ISKUUnitOfWork context,
    SKUConfigStatusEnum status = SKUConfigStatusEnum.Draft)
    {
        await context.SKUPartConfigRepository.AddAsync(
            context.GetTestData_SKUPartConfig(status)
        );
    }

    public static SKUPartConfig GetTestData_SKUPartConfig(this ISKUUnitOfWork context,
    SKUConfigStatusEnum status = SKUConfigStatusEnum.Draft)
    {
        return new SKUPartConfig
        {
            Id = 1,
            Name = "Test",
            Length = 3,
            GenericName = "Test",
            IsAlphaNumeric = true,
            IsCaseSensitive = true,
            SKUConfigId = 1,
            Status = status
        };
    }
    public static async Task AddTestData_SKUConfig(this ISKUUnitOfWork context,
        string name = "Test",
        SKUConfigStatusEnum status = SKUConfigStatusEnum.Draft)
    {
        await context.SKUConfigRepository.AddAsync(
            new SKUConfig
            {
                Id = 1,
                Name = name,
                Status = status
            }
        );
    }
    public static async Task AddTestData_SKUPartValues(this ISKUUnitOfWork context,
        string name = "Test", string uniqueCode = "TestCode")
    {
        await context.SKUPartValuesRepository.AddAsync(
            new SKUPartValues
            {
                Id = 1,
                SKUPartConfigId = 1,
                Name = name,
                UniqueCode = uniqueCode
            }
        );
    }
    public static async Task AddTestData_SKUConfigSequence(this ISKUUnitOfWork context,
    string name = "Test", int sKUConfigSequence = 0)
    {
        await context.SKUConfigSequenceRepository.AddAsync(
            new SKUConfigSequence
            {
                Id = 1,
                SKUPartConfigId = 1,
                Sequence = sKUConfigSequence
            }
        );
    }
}