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
            ServiceName = new ServiceName(serviceName),
            ServiceCode = new ServiceCode(code)
        };
        return serviceAggregate;
    }
    public void Update(string newServiceName, long newDefaultPrice)
    {
        this.ServiceName = new ServiceName(newServiceName);
        this.DefaultPrice = new Price(newDefaultPrice);
        this.DefaultPricecHistory.Add(new PriceHistory(new Price(newDefaultPrice), DateTime.UtcNow));

    }    
    public ServiceName ServiceName { get; set; }
    public ServiceCode ServiceCode { get; set; }
    public Price? DefaultPrice { get; set; }
    public List<PriceHistory> DefaultPricecHistory { get; set; } = new();


}
