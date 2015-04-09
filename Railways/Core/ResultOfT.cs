using JetBrains.Annotations;

namespace Railways.Core
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

            private set
            {

                this.value = value;
            }

        }

        public new static Result<T> Fail(ErrorMessage message)
        {
            var result = new Result<T>();
            result.Failed(message);

            return result;
        }

        public static Result<T> Ok([NotNull] T value)
        {
            Check.NotNull(value, "value");
            
            var result = new Result<T>();
            result.Succeeded(value);

            return result;
        }

        public static implicit operator Result<T>(ErrorMessage message)
        {
            return Fail<T>(message);
        }
        
        private void Succeeded(T value)
        {
            Succeeded();
            Value = value;
        }
    }
}