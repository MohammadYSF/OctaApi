namespace OctaApi.Domain.Models;

public class InventoryItem
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public string Name{ get; set; }
    public long? BuyPrice { get; set; }
    public long? SellPrice { get; set; }
    public float? Count{ get; set; }
    public bool IsActive { get; set; }
    public DateTime RegisterDate { get; set; }

    //public string BuyFactorCode { get; set; }
    //public string SellerName { get; set; }

    public float? CountLowerBound { get; set; }

    public virtual ICollection<InventoryItemHistory>  InventoryItemHistories{ get; set; }
    public virtual ICollection<InvoiceInventoryItem>   InvoiceInventoryItems{ get; set; }
}
