using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;
public class InventoryItemAddedToSellInvoiceEvent:DomainEvent
{
    public InventoryItemAddedToSellInvoiceEvent() : base(nameof(InventoryItemAddedToSellInvoiceEvent))
    {

    }
    //TODO
    public Guid SellInvoiceInventoryItemId { get; set; }
    public Guid SellInvoiceId { get; set; }
    public Guid InventoryItemId { get; set; }
    public float Count{ get; set; }
}
