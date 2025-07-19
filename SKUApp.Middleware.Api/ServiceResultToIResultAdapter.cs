using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using SKUApp.Common;
using SKUApp.Common.ErrorHandling;

namespace SKUApp.Middleware.Api
{
    public static class ServiceResultToIResultAdapter
    {
        public static IResult Adapt<T>(ServiceResult<T> serviceResult)
        {
            if (serviceResult == null)
                return Results.Problem("ServiceResult is null.");

            if (serviceResult.IsSuccess)
            {
                if (serviceResult.Value is not null)
                    return Results.Ok(serviceResult.Value);
                else
                    return Results.NoContent();
            }
            else
            {
                if (serviceResult.Error.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return Results.NotFound();
                }
                else if (serviceResult.Error.ErrorCode == (int)HttpStatusCode.UnprocessableEntity)
                {
                    return Results.UnprocessableEntity(serviceResult.Error.ValidationResults);
                }
                else if (serviceResult.Error.ErrorCode == (int)HttpStatusCode.BadRequest)
                {
                    return Results.BadRequest(serviceResult.Error.Message);
                }
                else
                {
                    return Results.Problem(serviceResult.Error.Message);
                }
            }
        }
    }
}