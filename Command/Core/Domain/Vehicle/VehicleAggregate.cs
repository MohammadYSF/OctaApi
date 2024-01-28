using Command.Core.Domain.Core;
using Command.Core.Domain.Vehicle.ValueObjects;
using OctaShared.Events;
namespace Command.Core.Domain.Vehicle;
public class VehicleAggregate : AggregateRoot
{
    public static VehicleAggregate Create(Guid id, int code, string name, string plate, string color,Guid customerId)
    {
        var aggregate = new VehicleAggregate
        {
            Id = id,
            Code = new VehicleCode(code),
            Color = new VehicleColor(color),
            Name = new VehicleName(name),
            Plate = new VehiclePlate(plate)
        };
        aggregate.AddDomainEvent(new VehicleCreatedEvent
        {
            Code = code,
            Color = color,
            VehicleId = id,
            Plate = plate,
            Name = name,
            EventId = Guid.NewGuid(),
            CustomerId = customerId
        });
        return aggregate;
    }
    public VehicleCode Code { get; set; }
    public VehicleName Name { get; set; }
    public VehiclePlate Plate { get; set; }
    public VehicleColor Color { get; set; }
}
