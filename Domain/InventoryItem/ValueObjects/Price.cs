using OctaApi.Domain.Common;

namespace OctaApi.Domain.InventoryItem.ValueObjects;
public sealed class Price : ValueObject<Price>
{
    public Price(long value)
    {
        Value = value;
    }
    public long Value { get; set; }
    protected override bool EqualsCore(Price other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}