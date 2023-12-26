using OctaApi.Domain.Common;
namespace Query.Application.Events.SellInvoice;
public class ServiceRemovedFromSellInvoiceEvent : DomainEvent
{
    public ServiceRemovedFromSellInvoiceEvent() : base(nameof(ServiceRemovedFromSellInvoiceEvent))
    {

    }
    public Guid SellInvoiceServiceId { get; set; }
    public Guid SellInvoiceId { get; set; }
}
