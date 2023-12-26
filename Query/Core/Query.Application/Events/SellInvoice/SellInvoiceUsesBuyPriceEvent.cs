using OctaApi.Domain.Common;
namespace Query.Application.Events.SellInvoice;
public class SellInvoiceUsesBuyPriceEvent : DomainEvent
{
    public SellInvoiceUsesBuyPriceEvent() : base(nameof(SellInvoiceUsesBuyPriceEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
}
