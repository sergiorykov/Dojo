using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Railways.Core.Extensions;

namespace Railways.Core
{
    [DebuggerStepThrough]
    public static class ObjectExtensions
    {
        [ContractAnnotation("value:null => halt")]
        public static void EnsureNotNull<T>([NoEnumeration] this T value,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            Check.NotNull(value, parameterName);
        }

        [ContractAnnotation("value:null => halt")]
        public static void EnsureNotEmpty<T>(IReadOnlyList<T> value,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            Check.NotEmpty(value, parameterName);
        }

        [ContractAnnotation("value:null => halt")]
        public static void EnsureNotEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            Check.NotEmpty(value, parameterName);
        }

        public static void EnsureIsDefined<T>(T value, [InvokerParameterName] [NotNull] string parameterName)
            where T : struct
        {
            Check.IsDefined(value, parameterName);
        }
    }

    [DebuggerStepThrough]
    public static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static void NotNull<T>([NoEnumeration] T value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        [ContractAnnotation("value:false => halt")]
        public static void Require(bool value, [CanBeNull] string message = "")
        {
            if (!value)
            {
                throw new ArgumentException(message);
            }
        }

        [ContractAnnotation("value:null => halt")]
        public static IReadOnlyList<T> NotEmpty<T>(IReadOnlyList<T> value,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Count == 0)
            {
                NotEmpty(parameterName, "parameterName");
                throw new ArgumentException("Collection '{0}' is empty".FormatWith(parameterName));
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            Exception e = null;
            if (ReferenceEquals(value, null))
            {
                e = new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                e = new ArgumentException("Argument '{0}' is empty".FormatWith(parameterName));
            }

            if (e != null)
            {
                NotEmpty(parameterName, "parameterName");
                throw e;
            }

            return value;
        }

        public static T IsDefined<T>(T value, [InvokerParameterName] [NotNull] string parameterName)
            where T : struct
        {
            if (!Enum.IsDefined(typeof (T), value))
            {
                NotEmpty(parameterName, "parameterName");
                throw new ArgumentException("Invalid enum value '{0}' of {1}".FormatWith(parameterName,
                    typeof (T).FullName));
            }

            return value;
        }
    }
}