using skudatabase.domain.Infrastructure;
using skudatabase.domain.Models;

namespace skudatabase.domain.DataLayer;

public static class ISKUUnitOfWorkExtenstions
{
    public static async Task AddTestData_SKUPartConfig(this ISKUUnitOfWork context)
    {
        await context.SKUPartConfigRepository.AddAsync(
            context.GetTestData_SKUPartConfig()
        );
    }

    public static SKUPartConfig GetTestData_SKUPartConfig(this ISKUUnitOfWork context)
    {
        return new SKUPartConfig
        {
            Id = 1,
            Name = "Test",
            Length = 3,
            GenericName = "Test",
            IsAlphaNumeric = true,
            IsCaseSensitive = true,
            SKUConfigId = 1
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
}