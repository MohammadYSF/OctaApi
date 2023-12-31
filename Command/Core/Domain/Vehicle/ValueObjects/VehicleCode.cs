﻿using Command.Core.Domain.Core;

namespace Command.Core.Domain.Vehicle.ValueObjects;

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
