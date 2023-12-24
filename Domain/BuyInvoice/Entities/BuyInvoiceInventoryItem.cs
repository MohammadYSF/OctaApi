using OctaApi.Domain.Common;
namespace Domain.BuyInvoice.Entities;

public class BuyInvoiceInventoryItem : Entity
{
    public static BuyInvoiceInventoryItem Create(Guid id, Guid buyInvoiceId, Guid inventoryItemId, float count)
    {
        return new BuyInvoiceInventoryItem
        {
            Id = id,
            BuyInvoiceId = buyInvoiceId,
            InventoryItemId = inventoryItemId,
            Count = count
        };
    }
    public Guid BuyInvoiceId { get; set; }
    public Guid InventoryItemId { get; set; }
    public float Count { get; set; }
}
