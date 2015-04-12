using System;
using JetBrains.Annotations;
using Nelibur.Sword.DataStructures;
using Nelibur.Sword.Extensions;

namespace Railways.Core.Results
{
    public class Result
    {
        protected Result()
        {
        }

        public bool Success { get; private set; }
        public ErrorReason ErrorReason { get; private set; }

        public bool Failure
        {
            get { return !Success; }
        }

        protected void Failed(ErrorReason errorReason)
        {
            Check.NotNull(errorReason, "errorReason");

            Success = false;
            ErrorReason = errorReason;
        }

        protected void Succeeded()
        {
            Success = true;
        }

        public static Result Fail(ErrorReason reason)
        {
            var result = new Result();
            result.Failed(reason);

            return result;
        }

        public static Result Ok()
        {
            var result = new Result();
            result.Succeeded();

            return result;
        }

        public static Result<T> Fail<T>(ErrorReason reason)
        {
            return Result<T>.Fail(reason);
        }

        public static Result<T> Ok<T>([NotNull] T value)
        {
            return Result<T>.Ok(value);
        }

        public Result And(Result otherResult)
        {
            if (Failure)
            {
                return this;
            }

            return otherResult;
        }

        public Result And(Option<Result> otherResult)
        {
            if (Failure)
            {
                return this;
            }

            return otherResult.MapOnEmpty(() => this).Value;
        }

        public Result And<T>(Option<Result<T>> otherResult)
        {
            if (Failure)
            {
                return this;
            }

            return otherResult.ToType<Result>().MapOnEmpty(() => this).Value;
        }
    }
}
