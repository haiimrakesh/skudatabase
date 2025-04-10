namespace SKUApp.Common.ErrorHandling
{
    public partial class Error
    {
        public static Error NoError() => new Error(0, string.Empty);
        public static Error NotFound(string errorMessage) => new Error(404, errorMessage);
        public static Error BadRequest(string errorMessage) => new Error(400, errorMessage);
        public static Error InternalServerError(string errorMessage) => new Error(500, errorMessage);
    }
}