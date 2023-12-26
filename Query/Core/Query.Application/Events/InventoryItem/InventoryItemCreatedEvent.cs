using Query.Application.Core;

namespace Query.Application.Events.InventoryItem;
public class InventoryItemCreatedEvent : DomainEvent
{
    public InventoryItemCreatedEvent() : base(nameof(InventoryItemCreatedEvent))
    {
    }
    public Guid InventoryItemId { get; set; }
    public DateTime CreateDateTime { get; set; }
    public int Code { get; set; }
    public string Name{ get; set; }
}
