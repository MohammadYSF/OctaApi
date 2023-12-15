using OctaApi.Domain.Common;

namespace Domain.Customer.ValueObjects;

public sealed class CustomerLastName : ValueObject<CustomerLastName>
{
    public CustomerLastName(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(CustomerLastName other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
