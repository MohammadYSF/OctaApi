using OctaApi.Domain.Common;
namespace Domain.BuyInvoice.Entities;

public class BuyInvoiceInventoryItem:Entity
{
    public Guid SellInvoiceId { get; set; }
    public Guid InventoryItemId { get; set; }
    public float Count { get; set; }
}
