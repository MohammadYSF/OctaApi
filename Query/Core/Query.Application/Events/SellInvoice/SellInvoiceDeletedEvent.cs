using Query.Application.Core;
namespace Query.Application.Events.SellInvoice;
public class SellInvoiceDeletedEvent : DomainEvent
{
    public SellInvoiceDeletedEvent() : base(nameof(SellInvoiceDeletedEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
}
