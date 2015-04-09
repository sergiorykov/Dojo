using System;
using Railways.Core;

namespace Railways.Errors
{
    public sealed class CustomerNameMustNotBeLongerThen : ErrorMessage
    {
        public CustomerNameMustNotBeLongerThen(int value)
        {
            Check.Require(value >= 0, "value >= 0");
            MaxLength = value;
        }

        public int MaxLength { get; private set; }
    }
}
