using Command.Core.Domain.Core;
using Command.Core.Domain.SellInvoice.Entities;
using Command.Core.Domain.SellInvoice.Events;
using Command.Core.Domain.SellInvoice.ValueObjects;

namespace Command.Core.Domain.SellInvoice;

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
    public SellInvoicecDescription Description { get; set; } = new(string.Empty);
    public List<SellInvoicePayment> Payments { get; set; } = new();
    public void Pay(long amount, DateTime payDate, string trackCode, long paidSoFar, long total)
    {
        if (paidSoFar + amount > total) throw new Exception(""); //todo
        var id = Guid.NewGuid();
        SellInvoicePaymentDate pdate = new(payDate);
        SellInvoicePaymentTrackCode pTrackCode = new(trackCode);
        SellInvoicePaidAmount pAmount = new(amount);
        Payments.Add(new SellInvoicePayment(id, this.Id, pdate, pTrackCode, pAmount));
        this.AddDomainEvent(new SellInvoicePaymentCreatedEvent
        {
            Amount = amount,
            EventId = Guid.NewGuid(),
            PayDate = payDate,
            SellInvoiceId = this.Id,
            TrackCode = trackCode,
        });
    }
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
        agg.AddDomainEvent(new SellInvoiceCreatedEvent
        {
            CreatedDate = createDate,
            CustomerId = Guid.Empty,
            EventId = Guid.NewGuid(),
            SellInvoiceCode = code.ToString(),
            SellInvoiceId = id,
            VehicleId = Guid.Empty,
        });
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
        if (this.IsClosed) throw new Exception(); //todo throw exception
        this.AddDomainEvent(new SellInvoiceDeletedEvent
        {
            EventId = Guid.NewGuid(),
            SellInvoiceId = this.Id
        });
    }
    public void SetUseBuyPrice(bool useBuyPrice)
    {
        if (useBuyPrice && (this.UseBuyPrice != useBuyPrice))
        {
            this.AddDomainEvent(new SellInvoiceUsesBuyPriceEvent
            {
                EventId = Guid.NewGuid(),
                SellInvoiceId = this.Id
            });
            this.UseBuyPrice = useBuyPrice;
        }

        else if ((!useBuyPrice) && (this.UseBuyPrice != useBuyPrice))
        {
            this.AddDomainEvent(new SellInvoiceDoesNotUseBuyPriceEvent
            {
                EventId = Guid.NewGuid(),
                SellInvoiceId = this.Id
            });
            this.UseBuyPrice = useBuyPrice;

        }
    }
    public void UpdateDescription(string description)
    {
        if (this.Description.Value != description)
        {
            this.AddDomainEvent(new SellInvoiceUpDescriptionUpdatedEvent
            {
                EventId = Guid.NewGuid(),
                NewDescription = description,
                SellInvoiceId = this.Id
            });
            this.Description = new SellInvoicecDescription(description);
        }

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
        int oldCount = this.InventoryItems.Count;

        this.InventoryItems = this.InventoryItems.Where(a => a.Id != sellInvoiceInventoryItem).ToList();
        if (oldCount != this.InventoryItems.Count)
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
        int oldCount = this.Services.Count;
        this.Services = this.Services.Where(a => a.Id != sellInvoiceServiceId).ToList();
        if (this.Services.Count != oldCount)
            this.AddDomainEvent(new ServiceRemovedFromSellInvoiceEvent
            {
                EventId = Guid.NewGuid(),
                SellInvoiceId = this.Id,
                SellInvoiceServiceId = sellInvoiceServiceId
            });

        //TODO
    }
}
