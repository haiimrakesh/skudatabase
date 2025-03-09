using System.ComponentModel.DataAnnotations;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.Services;
using SKUApp.Middleware.Api.DTOs;

namespace SKUApp.Middleware.Api;

public static class SKUPartValueApi
{
    public static void MapSKUPartEntryEndpoints(this WebApplication app)
    {
        Func<SKUPartEntry, SKUPartEntryResponse> transformMethod = (SKUPartEntry config) =>
        {
            return new SKUPartEntryResponse
            {
                Id = config.Id,
                Name = config.Name,
                SKUPartConfigId = config.SKUPartConfigId,
                UniqueCode = config.UniqueCode
            };
        };

        _ = app.MapGet("/api/skupartentries/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUPartConfigService? sKUPartConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUPartConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResultFromEnumerable(
                await sKUPartConfigService.GetSKUPartEntriesByPartConfigIdAsync(Id), transformMethod);
        }).WithTags("SKUPartEntry").WithName("GetSKUPartEntriesByPartConfigId").WithOpenApi();

        _ = app.MapPost("/api/skupartentry", async (HttpContext context, CreateSKUPartEntryRequest config) =>
        {
            //Validate CreateSKUConfigRequest
            if (!ValidationHelper.Validate(config, out List<ValidationResult> validationResults))
            {
                return Results.BadRequest(validationResults);
            }

            ISKUPartConfigService? sKUPartConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUPartConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }

            SKUPartEntry sKUPartConfig = new SKUPartEntry
            {
                Id = 0,
                Name = config.Name,
                SKUPartConfigId = config.SKUPartConfigId,
                UniqueCode = config.UniqueCode
            };

            return ResultsTranslator.TranslateResult(await sKUPartConfigService.AddSKUPartEntryAsync(sKUPartConfig));
        }).WithTags("SKUPartEntry").WithName("PostSKUPartEntry").WithOpenApi();

        _ = app.MapDelete("/api/skupartentry/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUPartConfigService? sKUConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult(
                await sKUConfigService.DeleteSKUPartEntryAsync(Id),
                transformMethod);

        }).WithTags("SKUPartEntry").WithName("DeleteSKUPartEntry").WithOpenApi();
    }
}