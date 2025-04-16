using System.Net;
using SKUApp.Common.ErrorHandling;
namespace SKUApp.Middleware.Api
{
    public static class ResultsTranslator
    {
        private static IResult handleError<T>(Result<T> result)
        {
            if (result.Error.ErrorCode == (int)HttpStatusCode.NotFound)
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
        public static IResult TranslateResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            else
            {
                return handleError(result);
            }
        }
        public static IResult TranslateResult<T, RT>(Result<T> result, Func<T, RT> transform)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(transform(result.Value!));
            }
            else
            {
                return handleError(result);
            }
        }
        public static IResult TranslateResultFromEnumerable<T, RT>(Result<IEnumerable<T>> result, Func<T, RT> transform)
        {
            if (result.IsSuccess)
            {
                List<RT> transformed = new List<RT>();
                foreach (T item in result.Value!)
                {
                    transformed.Add(transform(item));
                }
                return Results.Ok(transformed);
            }
            else
            {
                return handleError(result);
            }
        }
        public static IResult TranslateResultFromEnumerable<T>(Result<IEnumerable<T>> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            else
            {
                return handleError(result);
            }
        }
    }
}