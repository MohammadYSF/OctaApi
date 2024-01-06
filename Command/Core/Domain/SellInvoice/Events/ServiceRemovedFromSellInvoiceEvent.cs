using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;
public class ServiceRemovedFromSellInvoiceEvent : DomainEvent
{
    public ServiceRemovedFromSellInvoiceEvent() : base(nameof(ServiceRemovedFromSellInvoiceEvent))
    {

    }
    public Guid SellInvoiceServiceId { get; set; }
    public Guid SellInvoiceId { get; set; }
}
