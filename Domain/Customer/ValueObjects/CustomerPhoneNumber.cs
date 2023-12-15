using OctaApi.Domain.Common;

namespace Domain.Customer.ValueObjects;
public sealed class CustomerPhoneNumber : ValueObject<CustomerPhoneNumber>
{
    public CustomerPhoneNumber(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(CustomerPhoneNumber other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
