
using Command.Core.Domain.Core;

namespace Command.Core.Domain.InventoryItem.ValueObjects;
public sealed class InventoryItemName : ValueObject<InventoryItemName>
{
    public InventoryItemName(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(InventoryItemName other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
    public override string ToString()
    {
        return this.Value;
    }
}