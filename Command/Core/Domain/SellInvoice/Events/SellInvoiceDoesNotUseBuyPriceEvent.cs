using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;

public class SellInvoiceDoesNotUseBuyPriceEvent : DomainEvent
{
    public SellInvoiceDoesNotUseBuyPriceEvent() : base(nameof(SellInvoiceDoesNotUseBuyPriceEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
}
