using OctaApi.Domain.Common;

namespace Domain.Customer.ValueObjects;

public sealed class VehicleCode : ValueObject<VehicleCode>
{
    public VehicleCode(int value)
    {
        Value = value;
    }
    public int Value { get; set; }
    protected override bool EqualsCore(VehicleCode other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
