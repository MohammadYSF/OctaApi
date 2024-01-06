using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;
public class SellInvoiceClosedEvent : DomainEvent
{
    public SellInvoiceClosedEvent() : base(nameof(SellInvoiceClosedEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
}
