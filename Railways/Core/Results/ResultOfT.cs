using System;
using JetBrains.Annotations;

namespace Railways.Core.Results
{
    public sealed class Result<T> : Result
    {
        private T value;

        public T Value
        {
            get
            {
                Check.Require(Success);
                return value;
            }

            private set { this.value = value; }
        }

        public new static Result<T> Fail(ErrorReason reason)
        {
            var result = new Result<T>();
            result.Failed(reason);

            return result;
        }

        public static Result<T> Ok([NotNull] T value)
        {
            Check.NotNull(value, "value");

            var result = new Result<T>();
            result.Succeeded();
            result.Value = value;

            return result;
        }

        public static implicit operator Result<T>(ErrorReason reason)
        {
            return Fail<T>(reason);
        }
    }
}
