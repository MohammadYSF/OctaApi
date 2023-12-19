using OctaApi.Domain.Common;
namespace Domain.SellInvoice.Events;
public class SellInvoiceClosedEvent : DomainEvent
{
    public Guid SellInvoiceId { get; set; }
}
