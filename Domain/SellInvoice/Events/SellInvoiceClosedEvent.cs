using OctaApi.Domain.Common;
namespace Domain.SellInvoice.Events;
public class SellInvoiceClosedEvent : DomainEvent
{
    public SellInvoiceClosedEvent() : base(nameof(SellInvoiceClosedEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
}
