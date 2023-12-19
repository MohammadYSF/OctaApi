using OctaApi.Domain.Common;

namespace OctaApi.Domain.InventoryItem.ValueObjects;
public sealed class InventoryItemCode : ValueObject<InventoryItemCode>
{
    public InventoryItemCode(int value)
    {
        Value = value;
    }
    public int Value { get; set; }
    protected override bool EqualsCore(InventoryItemCode other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
    public override string ToString()
    {
        return this.Value.ToString();
    }
}