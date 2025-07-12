namespace FitnessApp.Helpers
{
    public class Result
    {
        public bool Success { get; }
        public string Error { get; }
        public ErrorType ErrorType { get; }

        public bool IsFailure => !Success;

        protected Result(bool success, string error, ErrorType errorType)
        {
            if (success && !string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException();
            if (!success && string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException();

            Success = success;
            Error = error;
            ErrorType = errorType;
        }

        public static Result Fail(string message, ErrorType errorType = ErrorType.Internal)
            => new(false, message, errorType);

        public static Result<T> Fail<T>(string message, ErrorType errorType = ErrorType.Internal)
            => new(default!, false, message, errorType);

        public static Result Ok()
            => new(true, string.Empty, ErrorType.None);

        public static Result<T> Ok<T>(T value)
            => new(value, true, string.Empty, ErrorType.None);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        internal Result(T value, bool success, string error, ErrorType errorType)
            : base(success, error, errorType)
        {
            Value = value;
        }
    }
}
