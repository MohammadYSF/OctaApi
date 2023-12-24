using Domain.Core;
using Domain.Service.ValueObjects;
using OctaApi.Domain;
namespace Domain.Service;
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
        return serviceAggregate;
    }
    public void Delete()
    {
        this.IsActive = false;
    }
    public void Update(string newServiceName, long newDefaultPrice)
    {
        this.Name = new ServiceName(newServiceName);
        this.DefaultPrice = new Price(newDefaultPrice);
        this.DefaultPricecHistory.Add(new PriceHistory(new Price(newDefaultPrice), DateTime.UtcNow));

    }    
    public ServiceName Name { get; set; }
    public ServiceCode Code { get; set; }
    public Price DefaultPrice { get; set; } = new Price(0);
    public List<PriceHistory> DefaultPricecHistory { get; set; } = new();
    public bool IsActive { get; set; } = true;


}
