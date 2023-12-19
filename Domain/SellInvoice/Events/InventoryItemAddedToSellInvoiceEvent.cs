using OctaApi.Domain.Common;
namespace Domain.SellInvoice.Events;
public class InventoryItemAddedToSellInvoiceEvent:DomainEvent
{
    //TODO
    public Guid SellInvoiceInventoryItemId { get; set; }
    public Guid SellInvoiceId { get; set; }
    public Guid InventoryItemId { get; set; }
    public float Count{ get; set; }
}
