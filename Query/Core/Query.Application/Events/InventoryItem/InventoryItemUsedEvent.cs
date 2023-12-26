using Query.Application.Core;
namespace Query.Application.Events.InventoryItem;

public class InventoryItemUsedEvent : DomainEvent
{
    public InventoryItemUsedEvent() : base(nameof(InventoryItemUsedEvent))
    {
    }
    public Guid InventoryItemId { get; set; }
    public float Count { get; set; }
}
