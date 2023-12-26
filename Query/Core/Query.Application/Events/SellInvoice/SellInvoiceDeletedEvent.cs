using OctaApi.Domain.Common;
namespace Query.Application.Events.SellInvoice;
public class SellInvoiceDeletedEvent : DomainEvent
{
    public SellInvoiceDeletedEvent() : base(nameof(SellInvoiceDeletedEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
}
