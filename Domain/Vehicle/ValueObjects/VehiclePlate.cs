using OctaApi.Domain.Common;

namespace Domain.Vehicle.ValueObjects;
public sealed class VehiclePlate : ValueObject<VehiclePlate>
{
    public VehiclePlate(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(VehiclePlate other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
