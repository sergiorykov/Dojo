using Nelibur.Sword.DataStructures;
using Nelibur.Sword.Extensions;
using Railways.Core;
using Railways.Core.Extensions;
using Railways.Errors;

namespace Railways
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var request = new Request
            {
                CustomerName = "Leo",
                PhoneNumber = null
            };

            // http://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/
            // http://enterprisecraftsmanship.com/2015/02/26/exceptions-for-flow-control-in-c/
            // https://vimeo.com/113707214
            Result<CustomerName> customerName = CustomerName.Create(request.CustomerName);
            Option<Result<PhoneNumber>> phoneNumber = request.PhoneNumber.ToOption()
                .Map(x => PhoneNumber.Create(x));

            var resultBuilder = AggregateErrorResult.From(customerName);
            phoneNumber.Do(x => resultBuilder.Add(x));
            var result = resultBuilder.Create(() => new CustomerNotRegistered());

            //result.OnSuccess(x => RegisterInDb(x));
            //Result<CustomerName> customerName = CustomerName.Create("Leo");

        }

        private static Result RegisterInDb(CustomerName customerName)
        {
            return Result.Fail(new CustomerNameMustNotBeBlank());
        }

        private sealed class Request
        {
            public string CustomerName { get; set; }
            public string PhoneNumber { get; set; } 
        }

    }


    public sealed class Customer
    {
        public CustomerName Name { get; set; }
        public Option<PhoneNumber> PhoneNumber { get; set; }
    }

    public sealed class CustomerName
    {
        private const int MaxLength = 50;

        public CustomerName(string value)
        {
            Value = value.Trim();
        }

        public string Value { get; private set; }

        public static Result<CustomerName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new CustomerNameMustNotBeBlank();
            }

            if (value.Length > 50)
            {
                return new CustomerNameMustNotBeLongerThen(MaxLength);
            }

            return Result.Ok(new CustomerName(value));
        }
    }

    public sealed class PhoneNumber
    {
        public PhoneNumber(string value)
        {
            Value = value.Trim();
        }

        public string Value { get; private set; }

        public static Result<PhoneNumber> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 15)
            {
                return new PhoneNumberHasInvalidFormat();
            }

            return Result.Ok(new PhoneNumber(value));
        }
    }

}