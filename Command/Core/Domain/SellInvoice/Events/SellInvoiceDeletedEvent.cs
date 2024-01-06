using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;
public class SellInvoiceDeletedEvent : DomainEvent
{
    public SellInvoiceDeletedEvent() : base(nameof(SellInvoiceDeletedEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
}
