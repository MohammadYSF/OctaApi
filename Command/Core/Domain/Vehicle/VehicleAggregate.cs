using Command.Core.Domain.Core;
using Command.Core.Domain.Vehicle.ValueObjects;
namespace Command.Core.Domain.Vehicle;
public class VehicleAggregate : AggregateRoot
{
    public static VehicleAggregate Create(Guid id, int code, string name, string plate, string color)
    {
        var customerVehicle = new VehicleAggregate
        {
            Id = id,
            Code = new VehicleCode(code),
            Color = new VehicleColor(color),
            Name = new VehicleName(name),
            Plate = new VehiclePlate(plate)
        };
        return customerVehicle;
    }
    public VehicleCode Code { get; set; }
    public VehicleName Name { get; set; }
    public VehiclePlate Plate { get; set; }
    public VehicleColor Color { get; set; }
}
