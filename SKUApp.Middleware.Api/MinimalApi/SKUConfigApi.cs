using System.ComponentModel.DataAnnotations;
using System.Net;
using SKUApp.Common.ErrorHandling;
using SKUApp.Common.Validation;
using SKUApp.Domain.Entities;
using SKUApp.Domain.ServiceContracts;
using SKUApp.Presentation.DataTransferObjects.RequestResponse;
using SKUApp.Presentation.DataTransferObjects.ViewModels;

namespace SKUApp.Middleware.Api;

public static class SKUConfigApi
{
    public static void MapSKUConfigEndpoints(this WebApplication app)
    {
        _ = app.MapGet("/api/skuconfig/{Id}", async (HttpContext context, int Id, ISKUConfigService sKUConfigService) =>
        {
            return ResultsTranslator.TranslateResult(await sKUConfigService.GetSKUConfigByIdAsync(Id));
        })
        .WithTags("SKUConfig")
        .WithName("GetSKUConfigById")
        .WithOpenApi()
        .Produces(StatusCodes.Status200OK, typeof(SKUConfigViewModel), "application/json", "application/xml");

        _ = app.MapPost("/api/skuconfig/activate/{Id}", async (HttpContext context, int Id, ISKUConfigService sKUConfigService) =>
        {
            return ServiceResultToIResultAdapter.Adapt(
                await sKUConfigService.ActivateSKUConfigAsync(Id)
                );
        }).WithTags("SKUConfig").WithName("ActivateSKUConfigById").WithOpenApi();

        _ = app.MapPost("/api/skuconfig/deactivate/{Id}", async (HttpContext context, int Id, ISKUConfigService sKUConfigService) =>
        {
            return ServiceResultToIResultAdapter.Adapt(
                await sKUConfigService.DeactivateSKUConfigAsync(Id)
                );
        }).WithTags("SKUConfig").WithName("DeactivateSKUConfigById").WithOpenApi();

        _ = app.MapGet("/api/skuconfig", async (HttpContext context, ISKUConfigService sKUConfigService) =>
        {
            return ServiceResultToIResultAdapter.Adapt(await sKUConfigService.GetAllSKUConfigsAsync());
        }).WithTags("SKUConfig").WithName("GetSKUConfig").WithOpenApi();

        _ = app.MapPost("/api/skuconfig", async (HttpContext context, CreateSKUConfigRequest config, ISKUConfigService sKUConfigService) =>
        {
            return ResultsTranslator.TranslateResult(
                await sKUConfigService.AddSKUConfigAsync(config)
                );
        }).WithTags("SKUConfig").WithName("PostSKUConfig").WithOpenApi();

        _ = app.MapDelete("/api/skuconfig/{Id}", async (HttpContext context, int Id, ISKUConfigService sKUConfigService) =>
        {
            return ServiceResultToIResultAdapter.Adapt(
                await sKUConfigService.DeleteSKUConfigAsync(Id)
                );

        }).WithTags("SKUConfig").WithName("DeleteSKUConfig").WithOpenApi();
    }
}