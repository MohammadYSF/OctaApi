using OctaApi.Domain.Common;
namespace Query.Application.Events.Customer;
public class VehicleRemovedFromCustomer:DomainEvent
{
    public VehicleRemovedFromCustomer() : base(nameof(VehicleRemovedFromCustomer))
    {
    }
    public Guid VehicleId { get; set; }
    public Guid CustomerId { get; set; }
}
