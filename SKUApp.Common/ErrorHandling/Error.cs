using System.ComponentModel.DataAnnotations;

namespace SKUApp.Common.ErrorHandling
{
    public partial class Error
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public List<ValidationResult> ValidationResults { get; private set; } = new List<ValidationResult>();

        public Error(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}