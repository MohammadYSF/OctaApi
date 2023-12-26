using OctaApi.Domain.Common;
namespace Query.Application.Events.SellInvoice;
public class InventoryItemRemovedFromSellInvoicecEvent : DomainEvent
{
    public InventoryItemRemovedFromSellInvoicecEvent() : base(nameof(InventoryItemRemovedFromSellInvoicecEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
    public Guid SellInvoiceInventoryItemId { get; set; }
}
