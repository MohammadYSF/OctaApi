using Query.Application.Core;

namespace Query.Application.Events.Customer;
public class VehicleAddedToCustomerEvent : DomainEvent
{
    public VehicleAddedToCustomerEvent() : base(nameof(VehicleAddedToCustomerEvent))
    {
    }
    public Guid CustomerId { get; set; }
    public Guid VehicleId { get; set; }
    public string? VehiclePlate { get; set; }
    public string? VehicleColor { get; set; }
    public string? VehicleName { get; set; }

}
