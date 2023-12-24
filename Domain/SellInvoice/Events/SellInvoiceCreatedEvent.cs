using OctaApi.Domain.Common;
namespace Domain.SellInvoice.Events;
public class SellInvoiceCreatedEvent : DomainEvent
{
    public SellInvoiceCreatedEvent() : base(nameof(SellInvoiceCreatedEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid VehicleId{ get; set; }
    public string SellInvoiceCode { get; set; }
    public DateTime CreatedDate { get; set; }
}
