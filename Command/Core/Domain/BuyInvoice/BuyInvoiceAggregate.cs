using Command.Core.Domain.BuyInvoice.Entities;
using Command.Core.Domain.BuyInvoice.ValueObjects;
using Command.Core.Domain.Core;
using OctaShared.Events;

namespace Command.Core.Domain.Invoice;
public class BuyInvoiceAggregate : AggregateRoot
{
    public static BuyInvoiceAggregate Create(Guid id, DateTime buyDate, int code, string sellerName, List<BuyInvoiceInventoryItem> buyInvoiceInventoryItems)
    {

        var aggregate = new BuyInvoiceAggregate
        {
            Id = id,
            BuyDate = new BuyInvoiceBuyDate(buyDate),
            Code = new BuyInvoiceCode(code),
            SellerName = new BuyInvoiceSellerName(sellerName),
            InventoryItems = buyInvoiceInventoryItems,
        };
        float totalPrice = buyInvoiceInventoryItems.Sum(a => a.SellPrice);//todo
        aggregate.AddDomainEvent(new BuyInvoiceCreatedEvent
        {
            BuyDate = buyDate,
            BuyInvoiced = id,
            Code = code.ToString(),
            EventId = Guid.NewGuid(),
            SellerName = sellerName,
            TotalPrice = (long)totalPrice,
        });
        return aggregate;

    }
    public BuyInvoiceBuyDate BuyDate { get; set; }
    public BuyInvoiceCode Code { get; set; }
    public BuyInvoiceSellerName SellerName { get; set; }
    public List<BuyInvoiceInventoryItem> InventoryItems { get; set; } = new();
}
