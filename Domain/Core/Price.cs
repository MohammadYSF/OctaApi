using OctaApi.Domain.Common;

namespace Domain.Core;
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
    public override string ToString()
    {
        return Value.ToString();
    }
}