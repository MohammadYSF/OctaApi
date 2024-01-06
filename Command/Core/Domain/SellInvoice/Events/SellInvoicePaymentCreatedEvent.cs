
using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;

public class SellInvoicePaymentCreatedEvent : DomainEvent
{
    public SellInvoicePaymentCreatedEvent() : base(nameof(SellInvoicePaymentCreatedEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
    public long Amount { get; set; }
    public DateTime PayDate { get; set; }
    public string TrackCode { get; set; }
}
