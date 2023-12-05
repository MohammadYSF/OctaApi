using System.Runtime.InteropServices;
using OctaApi.Domain.Common;
using OctaApi.Domain.InventoryItem.ValueObjects;

namespace OctaApi.Domain.InventoryItem;

public class InventoryItem : AggregateRoot
{
    public static InventoryItem Create(Guid id, string name, int code)
    {
        return new InventoryItem
        {
            Id = id,
            Name = new InventoryItemName(name),
            Code = new InventoryItemCode(code),
            Count = new InventoryItemCount(0f),
            IsActive = true,
            RegisterDate = DateTime.UtcNow,
            BuyPriceHistory = new(),
            SellPriceHistory = new()
        };
    }
    public void Buy(long buyPrice, long sellPrice, float count)
    {
        this.BuyPrice = new Price(buyPrice);
        this.SellPrice = new Price(sellPrice);
        this.Count = new InventoryItemCount(count);
        this.BuyPriceHistory.Add(new PriceHistory(new Price(buyPrice), DateTime.UtcNow));
        this.SellPriceHistory.Add(new PriceHistory(new Price(sellPrice), DateTime.UtcNow));
        //TODO
    }
    public void Update(string name,long newBuyPrice, long newSellPrice, float newCount)
    {
        this.Name = new InventoryItemName(name);
        this.BuyPrice = new Price(newBuyPrice);
        this.SellPrice = new Price(newSellPrice);
        this.Count = new InventoryItemCount(newCount);
        this.BuyPriceHistory.Add(new PriceHistory(new Price(newBuyPrice),DateTime.UtcNow));
        this.SellPriceHistory.Add(new PriceHistory(new Price(newSellPrice),DateTime.UtcNow));
    }
    //public Guid Id { get; set; }
    public List<PriceHistory> BuyPriceHistory { get; set; }
    public List<PriceHistory> SellPriceHistory { get; set; }
    public InventoryItemCode Code { get; set; }
    public InventoryItemName Name { get; set; }
    public Price? BuyPrice { get; set; }
    public Price? SellPrice { get; set; }
    public InventoryItemCount Count { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegisterDate { get; set; }

    //public string BuyFactorCode { get; set; }
    //public string SellerName { get; set; }

    public float? CountLowerBound { get; set; }

    // public virtual ICollection<InventoryItemHistory> InventoryItemHistories { get; set; }
    //public virtual ICollection<InvoiceInventoryItem> InvoiceInventoryItems { get; set; }
}
