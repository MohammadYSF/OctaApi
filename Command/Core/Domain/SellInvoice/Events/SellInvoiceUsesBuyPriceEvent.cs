using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;

public class SellInvoiceUsesBuyPriceEvent : DomainEvent
{
    public SellInvoiceUsesBuyPriceEvent() : base(nameof(SellInvoiceUsesBuyPriceEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
}
