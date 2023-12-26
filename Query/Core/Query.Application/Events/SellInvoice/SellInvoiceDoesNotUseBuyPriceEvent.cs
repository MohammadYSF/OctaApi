using OctaApi.Domain.Common;
namespace Query.Application.Events.SellInvoice;
public class SellInvoiceDoesNotUseBuyPriceEvent : DomainEvent
{
    public SellInvoiceDoesNotUseBuyPriceEvent() : base(nameof(SellInvoiceDoesNotUseBuyPriceEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
}
