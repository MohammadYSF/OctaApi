using Query.Application.Core;
namespace Query.Application.Events.SellInvoice;
public class SellInvoiceClosedEvent : DomainEvent
{
    public SellInvoiceClosedEvent() : base(nameof(SellInvoiceClosedEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
}
