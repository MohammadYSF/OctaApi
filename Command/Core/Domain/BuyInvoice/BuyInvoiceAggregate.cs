using Command.Core.Domain.BuyInvoice.Entities;
using Command.Core.Domain.BuyInvoice.ValueObjects;
using Command.Core.Domain.Core;

namespace Command.Core.Domain.Invoice;
public class BuyInvoiceAggregate : AggregateRoot
{
    public static BuyInvoiceAggregate Create(Guid id, DateTime buyDate, int code, string sellerName, List<BuyInvoiceInventoryItem> buyInvoiceInventoryItems)
    {
        return new BuyInvoiceAggregate
        {
            Id = id,
            BuyDate = new BuyInvoiceBuyDate(buyDate),
            Code = new BuyInvoiceCode(code),
            SellerName = new BuyInvoiceSellerName(sellerName),
            InventoryItems = buyInvoiceInventoryItems,
        };
    }
    public BuyInvoiceBuyDate BuyDate { get; set; }
    public BuyInvoiceCode Code { get; set; }
    public BuyInvoiceSellerName SellerName { get; set; }
    public List<BuyInvoiceInventoryItem> InventoryItems { get; set; } = new();
}
