
using Query.Application.Core;

namespace Query.Application.Events.BuyInvoice;
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
