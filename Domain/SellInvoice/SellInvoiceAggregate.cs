using Domain.Core;
using Domain.SellInvoice.Entities;
using Domain.SellInvoice.Events;
using Domain.SellInvoice.ValueObjects;
using OctaApi.Domain;
using System.Runtime.CompilerServices;
namespace Domain.SellInvoice;

public class SellInvoiceAggregate : AggregateRoot
{
    public bool IsClosed
    {
        get
        {
            var dtNow = DateTime.UtcNow;
            return CreateDate.Value.Year == dtNow.Year &&
                CreateDate.Value.Month == dtNow.Month &&
                CreateDate.Value.Day != dtNow.Day;
        }
    }
    public bool IsMiscellaneous
    {
        get
        {
            return this.Vehicle == Guid.Empty && this.Vehicle == Guid.Empty;
        }
    }
    public SellInvoiceSellDate CreateDate { get; set; }
    public SellInvoiceCode Code { get; set; }
    public List<SellInvoiceInventoryItem> InventoryItems { get; set; } = new();
    public List<SellInvoiceService> Services { get; set; } = new();
    public Guid? Customer { get; set; }
    public Guid? Vehicle { get; set; }
    public bool UseBuyPrice { get; set; } = false;
    public Price Discount { get; set; }
    public SellInvoicecDescription Description { get; set; }
    public List<SellInvoicePayment> Payments { get; set; }
    public static SellInvoiceAggregate CreateMiscellaneous(Guid id, DateTime createDate, int code)
    {
        var agg = new SellInvoiceAggregate
        {
            Id = id,
            CreateDate = new SellInvoiceSellDate(createDate),
            Code = new SellInvoiceCode(code),
            Customer = Guid.Empty,
            Vehicle = Guid.Empty,
        };
        return agg;
    }
    public static SellInvoiceAggregate Create(Guid id, DateTime createDate, int code, Guid customer, Guid vehicle)
    {
        var agg = new SellInvoiceAggregate
        {
            Id = id,
            CreateDate = new SellInvoiceSellDate(createDate),
            Code = new SellInvoiceCode(code),
            Customer = customer,
            Vehicle = vehicle,
        };
        agg.AddDomainEvent(new SellInvoiceCreatedEvent
        {
            CreatedDate = createDate,
            CustomerId = customer,
            VehicleId = vehicle,
            EventId = Guid.NewGuid(),
            SellInvoiceCode = code.ToString(),
            SellInvoiceId = id
        });
        return agg;
    }
    public void Delete()
    {
        if (this.IsClosed) return; //todo throw exception
        this.AddDomainEvent(new SellInvoiceDeletedEvent
        {
            EventId = Guid.NewGuid(),
            SellInvoiceId = this.Id            
        });
    }
    public void SetUseBuyPrice(bool useBuyPrice)
    {
        this.UseBuyPrice = useBuyPrice;
        if (UseBuyPrice && (this.UseBuyPrice != useBuyPrice))
            this.AddDomainEvent(new SellInvoiceUsesBuyPriceEvent
            {
                EventId = Guid.NewGuid(),
                SellInvoiceId = this.Id
            });
        else if ((!useBuyPrice) && (this.UseBuyPrice != useBuyPrice))
            this.AddDomainEvent(new SellInvoiceDoesNotUseBuyPriceEvent
            {
                EventId = Guid.NewGuid(),
                SellInvoiceId = this.Id
            });
    }
    public void UpdateDescription(string description)
    {
        this.Description = new SellInvoicecDescription(description);
        if (this.Description.Value != description)
            this.AddDomainEvent(new SellInvoiceUpDescriptionUpdatedEvent
            {
                EventId = Guid.NewGuid(),
                NewDescription = description,
                SellInvoiceId = this.Id
            });
    }
    public void AddSellInvoiceInventoryItem(Guid sellInvoiceInventoryItemId, Guid inventoryItemId, float count)
    {

        this.InventoryItems.Add(new SellInvoiceInventoryItem(sellInvoiceInventoryItemId, this.Id, inventoryItemId));
        this.AddDomainEvent(new InventoryItemAddedToSellInvoiceEvent
        {
            EventId = Guid.NewGuid(),
            Count = count,
            SellInvoiceId = this.Id,
            InventoryItemId = inventoryItemId,
            SellInvoiceInventoryItemId = sellInvoiceInventoryItemId,
        });
        //TODO
    }
    public void RemoveSellInvoiceInventoryItem(Guid sellInvoiceInventoryItem)
    {
        this.InventoryItems = this.InventoryItems.Where(a => a.Id != sellInvoiceInventoryItem).ToList();
        this.AddDomainEvent(new InventoryItemRemovedFromSellInvoicecEvent
        {
            EventId = Guid.NewGuid(),
            SellInvoiceId = this.Id,
            SellInvoiceInventoryItemId = sellInvoiceInventoryItem
        });
        //TODO
    }
    public void AddSellInvoiceService(Guid sellInvoiceServiceId, Guid serviceId, long price)
    {
        this.Services.Add(new SellInvoiceService(sellInvoiceServiceId, this.Id, serviceId, new Price(price)));
        this.AddDomainEvent(new ServiceAddedToSellInvoiceEvent
        {
            EventId = Guid.NewGuid(),
            Price = price,
            SellInvoiceId = this.Id,
            SellInvoiceServiceId = sellInvoiceServiceId,
            ServiceId = serviceId
        });
        //TODO
    }
    public void RemoveSellInvoiceService(Guid sellInvoiceServiceId)
    {
        this.Services = this.Services.Where(a => a.Id != sellInvoiceServiceId).ToList();
        this.AddDomainEvent(new ServiceRemovedFromSellInvoiceEvent
        {
            EventId = Guid.NewGuid(),
            SellInvoiceId = this.Id,
            SellInvoiceServiceId = sellInvoiceServiceId
        });
        //TODO
    }
}
