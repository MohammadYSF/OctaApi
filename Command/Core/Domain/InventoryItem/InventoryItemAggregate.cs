using Command.Core.Domain.Core;
using Command.Core.Domain.InventoryItem.ValueObjects;
using OctaShared.Events;

namespace Command.Core.Domain.InventoryItem;

public class InventoryItemَAggregate : AggregateRoot
{
    public static InventoryItemَAggregate Create(Guid id, string name, int code)
    {
        var inventoryItemAggregate = new InventoryItemَAggregate
        {
            Id = id,
            Name = new InventoryItemName(name),
            Code = new InventoryItemCode(code),
        };
        inventoryItemAggregate.AddDomainEvent(new InventoryItemCreatedEvent
        {
            Code = code,
            CreateDateTime = DateTime.UtcNow,
            EventId = Guid.NewGuid(),
            InventoryItemId = id,
            Name = name,
        });
        return inventoryItemAggregate;
    }
    public void Delete()
    {
        this.IsActive = false;
    }
    public void Buy(long buyPrice, long sellPrice, float count)
    {
        this.BuyPrice = new Price(buyPrice);
        this.SellPrice = new Price(sellPrice);
        this.Count = new InventoryItemCount(this.Count.Value + count);
        this.BuyPriceHistory.Add(new PriceHistory(new Price(buyPrice), DateTime.UtcNow));
        this.SellPriceHistory.Add(new PriceHistory(new Price(sellPrice), DateTime.UtcNow));


        this.AddDomainEvent(new InventoryItemBoughtEvent
        {
            EventId = Guid.NewGuid(),
            BuyPrice = buyPrice,
            SellPrice = sellPrice,
            Code = this.Code.ToString(),
            Name = this.Name.ToString(),
        });
        //TODO
    }
    public void Update(string name, long newBuyPrice, long newSellPrice, float newCount)
    {
        this.Name = new InventoryItemName(name);
        this.BuyPrice = new Price(newBuyPrice);
        this.SellPrice = new Price(newSellPrice);
        this.Count = new InventoryItemCount(newCount);
        this.BuyPriceHistory.Add(new PriceHistory(new Price(newBuyPrice), DateTime.UtcNow));
        this.SellPriceHistory.Add(new PriceHistory(new Price(newSellPrice), DateTime.UtcNow));

        this.AddDomainEvent(new InventoryItemUpdatedEvent
        {
            InventoryItemId = this.Id,
            EventId = Guid.NewGuid(),
            NewBuyPrice = newBuyPrice,
            NewCount = newCount,
            NewName = name,
            NewSellPrice = newSellPrice,
            UpdateDate = DateTime.UtcNow,
            Code = this.Code.Value
        });
    }
    public void Use(float count)
    {
        this.Count = new InventoryItemCount(this.Count.Value - count);
    }
    public List<PriceHistory> BuyPriceHistory { get; set; } = new();
    public List<PriceHistory> SellPriceHistory { get; set; } = new();
    public InventoryItemCode Code { get; set; }
    public InventoryItemName Name { get; set; }
    public Price BuyPrice { get; set; } = new Price(0);
    public Price SellPrice { get; set; } = new Price(0);
    public InventoryItemCount Count { get; set; } = new InventoryItemCount(0);
    public bool IsActive { get; set; } = true;
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;


    public float CountLowerBound { get; set; } = 0;

}
