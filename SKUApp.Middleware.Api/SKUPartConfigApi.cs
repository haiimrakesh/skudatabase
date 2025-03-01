using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.UnitOfWork;
using SKUApp.Domain.Services;

namespace SKUApp.Middleware.Api;

public static class SKUPartConfigApi
{
    public static void MapSKUPartConfigEndpoints(this WebApplication app)
    {
        _ = app.MapGet("/api/skupartconfig", async (HttpContext context) =>
        {
            ISKUUnitOfWork? sKUUnitOfWork = context.RequestServices.GetService<ISKUUnitOfWork>();
            if (sKUUnitOfWork == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            SKUPartConfigService service = new(sKUUnitOfWork);
            return Results.Ok(await service.GetAllSKUPartConfigsAsync());
        }).WithName("GetSKUPartConfig").WithOpenApi();

        _ = app.MapPost("/api/skupartconfig", async (HttpContext context, SKUPartConfig config) =>
        {
            ISKUUnitOfWork? sKUUnitOfWork = context.RequestServices.GetService<ISKUUnitOfWork>();
            if (sKUUnitOfWork == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            SKUPartConfigService service = new(sKUUnitOfWork);
            await service.AddSKUPartConfigAsync(config);
            return Results.Ok(await service.GetAllSKUPartConfigsAsync());
        }).WithName("PostSKUPartConfig").WithOpenApi();
    }
}