using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.Entities;
public class SellInvoiceInventoryItem : Entity
{
    public SellInvoiceInventoryItem(Guid id , Guid sellInvoiceId , Guid inventoryItemId)
    {
        this.SellInvoiceId = sellInvoiceId;
        this.InventoryItemId = inventoryItemId;
        this.Id = id;
    }
    public Guid SellInvoiceId{ get; private set; }
    public Guid InventoryItemId{ get; private set; }
    public float Count { get; private set; } = 0;
}
