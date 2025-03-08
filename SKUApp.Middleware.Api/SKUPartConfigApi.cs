using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.Services;

namespace SKUApp.Middleware.Api;

public static class SKUPartConfigApi
{
    public static void MapSKUPartConfigEndpoints(this WebApplication app)
    {
        _ = app.MapGet("/api/skupartconfig", async (HttpContext context) =>
        {
            ISKUPartConfigService? sKUPartConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUPartConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult(await sKUPartConfigService.GetAllSKUPartConfigsAsync());
        }).WithName("GetSKUPartConfig").WithOpenApi();

        _ = app.MapGet("/api/skupartconfig/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUPartConfigService? sKUPartConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUPartConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult(await sKUPartConfigService.GetSKUPartConfigByIdAsync(Id));
        }).WithName("GetSKUPartConfigById").WithOpenApi();

        _ = app.MapPost("/api/skupartconfig", async (HttpContext context, SKUPartConfig config) =>
        {
            ISKUPartConfigService? sKUPartConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUPartConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult(await sKUPartConfigService.AddSKUPartConfigAsync(config));
        }).WithName("PostSKUPartConfig").WithOpenApi();
    }
}