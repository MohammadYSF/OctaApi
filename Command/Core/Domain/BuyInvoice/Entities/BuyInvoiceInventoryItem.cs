using Command.Core.Domain.Core;

namespace Command.Core.Domain.BuyInvoice.Entities;

public class BuyInvoiceInventoryItem : Entity
{
    public static BuyInvoiceInventoryItem Create(Guid id, Guid buyInvoiceId, Guid inventoryItemId, float count, long buyPrice, long sellPrice)
    {
        return new BuyInvoiceInventoryItem
        {
            Id = id,
            BuyInvoiceId = buyInvoiceId,
            InventoryItemId = inventoryItemId,
            Count = count,
            SellPrice = sellPrice,
            BuyPrice = buyPrice
        };
    }
    public float BuyPrice { get; set; }
    public float SellPrice { get; set; }
    public Guid BuyInvoiceId { get; set; }
    public Guid InventoryItemId { get; set; }
    public float Count
    {
        get; set;

    }
}
