using OctaApi.Domain.Common;
namespace Query.Application.Events.Vehicles;
public class SellInvoiceUsesBuyPriceEvent : DomainEvent
{
    public SellInvoiceUsesBuyPriceEvent() : base(nameof(SellInvoiceUsesBuyPriceEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
}
