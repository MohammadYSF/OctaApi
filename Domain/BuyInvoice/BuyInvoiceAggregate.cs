using Domain.BuyInvoice.ValueObjects;
using OctaApi.Domain;

namespace Domain.Invoice;
public class BuyInvoiceAggregate : AggregateRoot
{
    public BuyInvoiceBuyDate BuyDate { get; set; }
    public BuyInvoiceCode Code { get; set; }
    public BuyInvoiceSellerName SellerName { get; set; }
    List<Guid> InventoryItems { get; set; } = new();
}
