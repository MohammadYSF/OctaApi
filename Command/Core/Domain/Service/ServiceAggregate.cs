using Command.Core.Domain.Core;
using Command.Core.Domain.Service.ValueObjects;
using OctaShared.Events;
namespace Command.Core.Domain.Service;
public sealed class ServiceAggregate : AggregateRoot
{
    public static ServiceAggregate Create(Guid id, string serviceName, int code, long defaultPrice)
    {

        var serviceAggregate = new ServiceAggregate
        {
            Id = id,
            DefaultPrice = new Price(defaultPrice),
            Name = new ServiceName(serviceName),
            Code = new ServiceCode(code)
        };
        serviceAggregate.AddDomainEvent(new ServiceCreatedEvent
        {
            Code = code,
            CreateDateTime = DateTime.UtcNow,
            DefaultPrice = defaultPrice,
            EventId = Guid.NewGuid(),
            Name = serviceName,
            ServiceId = id,
        });
        return serviceAggregate;
    }
    public void Delete()
    {
        if (!this.IsActive) throw new Exception("");
        this.IsActive = false;
    }
    public void Update(string newServiceName, long newDefaultPrice)
    {
        var dateTimeNow = DateTime.UtcNow;
        this.Name = new ServiceName(newServiceName);
        this.DefaultPrice = new Price(newDefaultPrice);
        this.DefaultPricecHistory.Add(new PriceHistory(new Price(newDefaultPrice), dateTimeNow));
        this.AddDomainEvent(new ServiceUpdatedEvent
        {
            EventId = Guid.NewGuid(),
            NewDefaultPrice = newDefaultPrice,
            NewName = newServiceName,
            ServiceId = Guid.NewGuid(),
            UpdateDate = dateTimeNow,
            Code = this.Code.Value
        });

    }
    public ServiceName Name { get; set; }
    public ServiceCode Code { get; set; }
    public Price DefaultPrice { get; set; } = new Price(0);
    public List<PriceHistory> DefaultPricecHistory { get; set; } = new();
    public bool IsActive { get; set; } = true;


}
