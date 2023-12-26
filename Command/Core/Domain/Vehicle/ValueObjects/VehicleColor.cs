using Command.Core.Domain.Core;

namespace Command.Core.Domain.Vehicle.ValueObjects;
public sealed class VehicleColor : ValueObject<VehicleColor>
{
    public VehicleColor(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(VehicleColor other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
