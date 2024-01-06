using Command.Core.Domain.Core;

namespace Command.Core.Domain.BuyInvoice.Events;

public class BuyInvoiceCreatedEvent : DomainEvent
{
    public BuyInvoiceCreatedEvent() : base(nameof(BuyInvoiceCreatedEvent))
    {

    }

    public Guid BuyInvoiced { get; set; }
    public DateTime BuyDate { get; set; }
    public string Code { get; set; }
    public string SellerName { get; set; }
    public long TotalPrice { get; set; }
}
