using OctaApi.Domain.Common;
namespace Query.Application.Events.InventoryItem;
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
