using System.ComponentModel.DataAnnotations;
using System.Net;
using SKUApp.Domain.Entities;
using SKUApp.Domain.Infrastructure.ErrorHandling;
using SKUApp.Domain.Infrastructure.Services;
using SKUApp.Middleware.Api.DTOs;

namespace SKUApp.Middleware.Api;

public static class SKUConfigApi
{
    public static void MapSKUConfigEndpoints(this WebApplication app)
    {
        Func<SKUConfig, SKUConfigResponse> transformMethod = (SKUConfig config) =>
        {
            return new SKUConfigResponse
            {
                Id = config.Id,
                Name = config.Name,
                Length = config.Length,
                Status = config.Status.ToString(),
                Description = config.Description
            };
        };

        _ = app.MapGet("/api/skuconfig/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUConfigService? sKUConfigService = context.RequestServices.GetService<ISKUConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }

            Result<SKUConfig> sKUConfigResult = await sKUConfigService.GetSKUConfigByIdAsync(Id);
            if (!sKUConfigResult.IsSuccess)
            {
                return ResultsTranslator.TranslateResult(sKUConfigResult, transformMethod);
            }

            //resolve SKUConfigSequenceResponse
            List<SKUConfigSequenceResponse> skuConfigSequenceResponses = new List<SKUConfigSequenceResponse>();

            return ResultsTranslator.TranslateResult(
                sKUConfigResult, transformMethod);

        }).WithTags("SKUConfig").WithName("GetSKUConfigById").WithOpenApi();

        _ = app.MapPost("/api/skuconfig/activate/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUConfigService? sKUConfigService = context.RequestServices.GetService<ISKUConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }

            return ResultsTranslator.TranslateResult(
                await sKUConfigService.ActivateSKUConfigAsync(Id),
                transformMethod);
        }).WithTags("SKUConfig").WithName("ActivateSKUConfigById").WithOpenApi();

        _ = app.MapPost("/api/skuconfig/deactivate/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUConfigService? sKUConfigService = context.RequestServices.GetService<ISKUConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }

            return ResultsTranslator.TranslateResult(
                await sKUConfigService.DeactivateSKUConfigAsync(Id),
                transformMethod);
        }).WithTags("SKUConfig").WithName("DeactivateSKUConfigById").WithOpenApi();

        _ = app.MapGet("/api/skuconfig", async (HttpContext context) =>
        {
            ISKUConfigService? sKUConfigService = context.RequestServices.GetService<ISKUConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }

            return ResultsTranslator.TranslateResultFromEnumerable(
                await sKUConfigService.GetAllSKUConfigsAsync(),
                transformMethod);
        }).WithTags("SKUConfig").WithName("GetSKUConfig").WithOpenApi();

        _ = app.MapPost("/api/skuconfig", async (HttpContext context, CreateSKUConfigRequest config) =>
        {
            //Validate CreateSKUConfigRequest
            if (!ValidationHelper.Validate(config, out List<ValidationResult> validationResults))
            {
                return Results.BadRequest(validationResults);
            }

            ISKUConfigService? sKUConfigService = context.RequestServices.GetService<ISKUConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }

            SKUConfig sKUConfig = new SKUConfig
            {
                Id = 0,
                Name = config.Name,
                Description = config.Description,
                Length = config.Length,
                Status = SKUConfigStatusEnum.Draft
            };
            return ResultsTranslator.TranslateResult(
                await sKUConfigService.AddSKUConfigAsync(sKUConfig),
                transformMethod);
        }).WithTags("SKUConfig").WithName("PostSKUConfig").WithOpenApi();

        _ = app.MapDelete("/api/skuconfig/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUConfigService? sKUConfigService = context.RequestServices.GetService<ISKUConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult(
                await sKUConfigService.DeleteSKUConfigAsync(Id),
                transformMethod);

        }).WithTags("SKUConfig").WithName("DeleteSKUConfig").WithOpenApi();

        _ = app.MapPost("/api/skuconfig/sequence/{SkuConfigId}/{SkuPartConfigId}", async (HttpContext context, int SkuConfigId, int SkuPartConfigId) =>
        {
            ISKUConfigService? sKUConfigService = context.RequestServices.GetService<ISKUConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult(
                await sKUConfigService.AddSKUSequenceAsync(new SKUConfigSequence()
                {
                    Id = 0,
                    SKUConfigId = SkuConfigId,
                    SKUPartConfigId = SkuPartConfigId
                }));

        }).WithTags("SKUConfigSequence").WithName("SKUConfigAddSequence").WithOpenApi();

        _ = app.MapPost("/api/skuconfig/sequence/{Id}", async (HttpContext context, int Id) =>
        {
            ISKUConfigService? sKUConfigService = context.RequestServices.GetService<ISKUConfigService>();
            if (sKUConfigService == null)
            {
                return Results.Problem("Failed to retrieve SKUPartConfigService.");
            }
            return ResultsTranslator.TranslateResult(
                await sKUConfigService.DeleteSKUSequenceAsync(Id));

        }).WithTags("SKUConfigSequence").WithName("SKUConfigDeleteSequence").WithOpenApi();
    }
}