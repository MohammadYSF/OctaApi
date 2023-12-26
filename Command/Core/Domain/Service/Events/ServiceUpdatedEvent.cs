using Command.Core.Domain.Core;

namespace Command.Core.Domain.Service.Events;

public class ServiceUpdatedEvent:DomainEvent
{
    public ServiceUpdatedEvent() : base(nameof(ServiceUpdatedEvent))
    {

    }
    public Guid ServiceId { get; set; }
    public string NewName{ get; set; }
    public long NewDefaultPrice{ get; set; }
    public DateTime UpdateDate{ get; set; }
    public int Code { get; set; }
}
