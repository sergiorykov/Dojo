using System;
using JetBrains.Annotations;

namespace Railways.Core
{
    public class Result
    {
        protected Result()
        {
        }

        public bool Success { get; private set; }
        public ErrorMessage Error { get; private set; }

        public bool Failure
        {
            get { return !Success; }
        }

        protected void Failed(ErrorMessage errorMessage)
        {
            Check.NotNull(errorMessage, "errorMessage");

            Success = false;
            Error = errorMessage;
        }

        protected void Succeeded()
        {
            Success = true;
        }

        public static Result Fail(ErrorMessage message)
        {
            var result = new Result();
            result.Failed(message);

            return result;
        }

        public static Result Ok()
        {
            var result = new Result();
            result.Succeeded();

            return result;
        }

        public static Result<T> Fail<T>(ErrorMessage message)
        {
            return Result<T>.Fail(message);
        }

        public static Result<T> Ok<T>([NotNull] T value)
        {
            return Result<T>.Ok(value);
        }
    }
}
