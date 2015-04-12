using System;
using Railways.Core;
using Railways.Core.Results;

namespace Railways.Errors
{
    public sealed class CustomerNameMustNotBeLongerThen : ErrorReason
    {
        public CustomerNameMustNotBeLongerThen(int value)
        {
            Check.Require(value >= 0, "value >= 0");
            MaxLength = value;
        }

        public int MaxLength { get; private set; }
    }
}
