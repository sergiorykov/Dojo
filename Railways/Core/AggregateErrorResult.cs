using System;
using System.Collections.Generic;
using System.Linq;

namespace Railways.Core
{
    public sealed class AggregateErrorResult : Result
    {
        private AggregateErrorResult()
        {
            Errors = new List<ErrorMessage>();
        }

        public List<ErrorMessage> Errors { get; private set; }

        public static Builder From(Result result)
        {
            return new Builder().Add(result);
        }

        public sealed class Builder
        {
            private readonly List<ErrorMessage> errors = new List<ErrorMessage>();

            public Builder Add(Result result)
            {
                if (result.Failure)
                {
                    errors.Add(result.Error);
                }

                return this;
            }

            public AggregateErrorResult Create(Func<ErrorMessage> aggregatedError)
            {
                var result = new AggregateErrorResult();

                if (errors.Any())
                {
                    result.Failed(aggregatedError());
                    result.Errors.AddRange(errors);
                    return result;
                }

                result.Succeeded();
                return result;
            }
        }
    }
}