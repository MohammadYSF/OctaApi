using Command.Core.Domain.Core;

namespace Command.Core.Domain.InventoryItem.Events;

public class InventoryItemUsedEvent : DomainEvent
{
    public InventoryItemUsedEvent() : base(nameof(InventoryItemUsedEvent))
    {

    }
    public Guid InventoryItemId { get; set; }
    public float Count { get; set; }
}
