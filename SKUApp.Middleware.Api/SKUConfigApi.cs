using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.UnitOfWork;
using SKUApp.Domain.Services;

namespace SKUApp.Middleware.Api;

public static class SKUConfigApi
{
    public static void MapSKUConfigEndpoints(this WebApplication app)
    {
        _ = app.MapGet("/api/skuconfig", async (HttpContext context) =>
        {
            ISKUUnitOfWork? sKUUnitOfWork = context.RequestServices.GetService<ISKUUnitOfWork>();
            if (sKUUnitOfWork == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            SKUConfigService service = new(sKUUnitOfWork);
            return Results.Ok(await service.GetAllSKUConfigsAsync());
        }).WithName("GetSKUConfig").WithOpenApi();

        _ = app.MapPost("/api/skuconfig", async (HttpContext context, SKUConfig config) =>
        {
            ISKUUnitOfWork? sKUUnitOfWork = context.RequestServices.GetService<ISKUUnitOfWork>();
            if (sKUUnitOfWork == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            SKUConfigService service = new(sKUUnitOfWork);
            await service.AddSKUConfigAsync(config);
            return Results.Ok(await service.GetAllSKUConfigsAsync());
        }).WithName("PostSKUConfig").WithOpenApi();
    }
}