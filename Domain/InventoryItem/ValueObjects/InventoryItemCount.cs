using OctaApi.Domain.Common;

namespace OctaApi.Domain.InventoryItem.ValueObjects;
public sealed class InventoryItemCount : ValueObject<InventoryItemCount>
{
    public float Value { get; set; }
    public InventoryItemCount(float value)
    {
        Value = value;
    }
      
    protected override bool EqualsCore(InventoryItemCount other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}