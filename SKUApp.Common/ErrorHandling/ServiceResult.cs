using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SKUApp.Common.ErrorHandling
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public Error Error { get; private set; }
        protected ServiceResult(bool isSuccess, T? value, Error error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static ServiceResult<T> Success(T value)
        {
            return new ServiceResult<T>(true, value, Error.NoError());
        }

        public static ServiceResult<T> Failure(Error error)
        {
            return new ServiceResult<T>(false, default(T), error);
        }

        public static implicit operator ServiceResult<T>(T value)
        {
            return Success(value);
        }

        public static implicit operator ServiceResult<T>(Error error)
        {
            return Failure(error);
        }
    }
}