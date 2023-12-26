using Command.Core.Domain.Core;
namespace Command.Core.Domain.Customer.Events;

public class VehicleRemovedFromCustomer:DomainEvent
{
    public VehicleRemovedFromCustomer() : base(nameof(VehicleRemovedFromCustomer))
    {

    }
    public Guid VehicleId { get; set; }
    public Guid CustomerId { get; set; }
}
