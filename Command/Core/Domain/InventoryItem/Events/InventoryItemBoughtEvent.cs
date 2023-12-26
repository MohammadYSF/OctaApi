using Command.Core.Domain.Core;

namespace Command.Core.Domain.InventoryItem.Events;

public class InventoryItemBoughtEvent : DomainEvent
{
    public InventoryItemBoughtEvent() : base(nameof(InventoryItemBoughtEvent))
    {

    }
    public string Name { get; set; }
    public string Code { get; set; }
    public long BuyPrice { get; set; }
    public long SellPrice { get; set; }
}
