using System.ComponentModel.DataAnnotations;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.Services;
using SKUApp.Middleware.Api.DTOs;

namespace SKUApp.Middleware.Api;

public static class SKUPartConfigApi
{
    public static void MapSKUPartConfigEndpoints(this WebApplication app)
    {
        Func<SKUPartConfig, SKUPartConfigResponse> transformMethod = (SKUPartConfig config) =>
        {
            return new SKUPartConfigResponse
            {
                Id = config.Id,
                Name = config.Name,
                Length = config.Length,
                Status = config.Status.ToString(),
                Description = config.Description,
                AllowPreceedingZero = config.AllowPreceedingZero,
                IncludeSpacerAtTheEnd = config.IncludeSpacerAtTheEnd,
                IsAlphaNumeric = config.IsAlphaNumeric,
                IsCaseSensitive = config.IsCaseSensitive,
                RestrictConflictingLettersAndCharacters = config.RestrictConflictingLettersAndCharacters,
                GenericName = config.GenericName
            };
        };
        _ = app.MapGet("/api/skupartconfig", async (HttpContext context) =>
        {
            ISKUPartConfigService? sKUPartConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUPartConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResultFromEnumerable<SKUPartConfig, SKUPartConfigResponse>(
                await sKUPartConfigService.GetAllSKUPartConfigsAsync(), transformMethod);
        }).WithTags("SKUPartConfig").WithName("GetSKUPartConfig").WithOpenApi();

        _ = app.MapGet("/api/skupartconfig/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUPartConfigService? sKUPartConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUPartConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult<SKUPartConfig, SKUPartConfigResponse>(
                await sKUPartConfigService.GetSKUPartConfigByIdAsync(Id), transformMethod);
        }).WithTags("SKUPartConfig").WithName("GetSKUPartConfigById").WithOpenApi();

        _ = app.MapPost("/api/skupartconfig", async (HttpContext context, CreateSKUPartConfigRequest config) =>
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

            SKUPartConfig sKUPartConfig = new SKUPartConfig
            {
                Id = 0,
                Name = config.Name,
                Length = config.Length,
                Status = SKUConfigStatusEnum.Draft,
                Description = config.Description,
                AllowPreceedingZero = config.AllowPreceedingZero,
                IncludeSpacerAtTheEnd = config.IncludeSpacerAtTheEnd,
                IsAlphaNumeric = config.IsAlphaNumeric,
                IsCaseSensitive = config.IsCaseSensitive,
                RestrictConflictingLettersAndCharacters = config.RestrictConflictingLettersAndCharacters,
                GenericName = config.GenericName
            };

            return ResultsTranslator.TranslateResult(await sKUPartConfigService.AddSKUPartConfigAsync(sKUPartConfig));
        }).WithTags("SKUPartConfig").WithName("PostSKUPartConfig").WithOpenApi();

        _ = app.MapDelete("/api/skupartconfig/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUPartConfigService? sKUConfigService = context.RequestServices.GetService<ISKUPartConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult(
                await sKUConfigService.DeleteSKUPartConfigAsync(Id),
                transformMethod);

        }).WithTags("SKUPartConfig").WithName("DeleteSKUPartConfig").WithOpenApi();
    }
}