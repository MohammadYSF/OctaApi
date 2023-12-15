using OctaApi.Domain.Common;

namespace Domain.Customer.ValueObjects;
public sealed class CustomerCode : ValueObject<CustomerCode>
{
    public CustomerCode(int value)
    {
        Value = value;
    }
    public int Value { get; set; }
    protected override bool EqualsCore(CustomerCode other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}

