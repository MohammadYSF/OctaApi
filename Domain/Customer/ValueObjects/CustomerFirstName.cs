using OctaApi.Domain.Common;

namespace Domain.Customer.ValueObjects;

public sealed class CustomerFirstName : ValueObject<CustomerFirstName>
{
    public CustomerFirstName(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(CustomerFirstName other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
