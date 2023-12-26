using Query.Application.Core;

namespace Query.Application.Events.Services;
public class ServiceCreatedEvent:DomainEvent
{
    public ServiceCreatedEvent() : base(nameof(ServiceCreatedEvent))
    {
    }
    public Guid ServiceId { get; set; }
    public string Name{ get; set; }
    public long DefaultPrice{ get; set; }
    public int Code { get; set; }
    public DateTime CreateDateTime { get; set; }
}
