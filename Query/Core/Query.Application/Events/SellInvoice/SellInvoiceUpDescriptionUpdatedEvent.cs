using Query.Application.Core;

namespace Query.Application.Events.SellInvoice;
public class SellInvoiceUpDescriptionUpdatedEvent:DomainEvent
{
    public SellInvoiceUpDescriptionUpdatedEvent() : base(nameof(SellInvoiceUpDescriptionUpdatedEvent))
    {
    }
    public Guid SellInvoiceId { get; set; }
    public string NewDescription{ get; set; }
}
