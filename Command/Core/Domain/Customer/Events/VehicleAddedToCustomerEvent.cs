using Command.Core.Domain.Core;

namespace Command.Core.Domain.Customer.Events;

public class VehicleAddedToCustomerEvent : DomainEvent
{
    public VehicleAddedToCustomerEvent() : base(nameof(VehicleAddedToCustomerEvent))
    {

    }
    public Guid CustomerId { get; set; }
    public Guid VehicleId { get; set; }
    public string? VehiclePlate { get; set; }
    public string? VehicleColor{ get; set; }
    public string? VehicleName{ get; set; }

}
