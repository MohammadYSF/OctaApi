using Command.Core.Domain.Core;

namespace Command.Core.Domain.Vehicle.ValueObjects;
public sealed class VehicleName : ValueObject<VehicleName>
{
    public VehicleName(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(VehicleName other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
