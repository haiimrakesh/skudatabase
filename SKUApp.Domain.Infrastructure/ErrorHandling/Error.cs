namespace SKUApp.Domain.Infrastructure.ErrorHandling
{
    public partial class Error
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public Error(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}