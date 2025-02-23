namespace SKUApp.Domain.Infrastructure.ErrorHandling
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public Error Error { get; private set; }

        protected Result(bool isSuccess, T? value, Error error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, Error.NoError);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(false, default(T), error);
        }

        public static implicit operator Result<T>(T value)
        {
            return Success(value);
        }

        public static implicit operator Result<T>(Error error)
        {
            return Failure(error);
        }
    }
}