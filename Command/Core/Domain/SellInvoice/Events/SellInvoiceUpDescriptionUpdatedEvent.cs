using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Events;

public class SellInvoiceUpDescriptionUpdatedEvent : DomainEvent
{
    public SellInvoiceUpDescriptionUpdatedEvent() : base(nameof(SellInvoiceUpDescriptionUpdatedEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
    public string NewDescription { get; set; }
}
