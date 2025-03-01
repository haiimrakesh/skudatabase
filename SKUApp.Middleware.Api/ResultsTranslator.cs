using System.Net;
using SKUApp.Domain.Infrastructure.ErrorHandling;

namespace SKUApp.Middleware.Api
{
    public static class ResultsTranslator
    {
        public static IResult TranslateResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            else if (result.Error.ErrorCode == (int)HttpStatusCode.NotFound)
            {
                return Results.NotFound();
            }
            else if (result.Error.ErrorCode == (int)HttpStatusCode.BadRequest)
            {
                return Results.BadRequest(result.Error.Message);
            }
            else
            {
                return Results.Problem(result.Error.Message);
            }
        }
    }
}