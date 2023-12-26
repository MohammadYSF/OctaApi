using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;

public class InventoryItemRemovedFromSellInvoicecEvent : DomainEvent
{
    public InventoryItemRemovedFromSellInvoicecEvent() : base(nameof(InventoryItemRemovedFromSellInvoicecEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
    public Guid SellInvoiceInventoryItemId { get; set; }
}
