using Query.Application.Core;
public class SellInvoiceUsesBuyPriceEvent : DomainEvent
{
    public SellInvoiceUsesBuyPriceEvent() : base(nameof(SellInvoiceUsesBuyPriceEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
}
