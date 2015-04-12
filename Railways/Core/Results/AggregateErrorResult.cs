using System;
using System.Collections.Generic;
using System.Linq;

namespace Railways.Core.Results
{
    public sealed class AggregateErrorResult : Result
    {
        private AggregateErrorResult()
        {
            Errors = new List<ErrorReason>();
        }

        public List<ErrorReason> Errors { get; private set; }

        public static Builder From(Result result)
        {
            return new Builder().Add(result);
        }


        public sealed class Builder
        {
            private readonly List<ErrorReason> errors = new List<ErrorReason>();

            public Builder Add(Result result)
            {
                if (result.Failure)
                {
                    errors.Add(result.ErrorReason);
                }

                return this;
            }

            public AggregateErrorResult Create(Func<ErrorReason> aggregatedError)
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
