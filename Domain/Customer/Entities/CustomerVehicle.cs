using Domain.Customer.ValueObjects;
using OctaApi.Domain.Common;

namespace Domain.Customer.Entities;

public class CustomerVehicle:Entity
{
    public static CustomerVehicle Create(Guid id , int code , string name , string plate , string color)
    {
        var customerVehicle = new CustomerVehicle
        {
            Id = id,
            Code = new VehicleCode(code),
            Color = new VehicleColor(color),
            Name = new VehicleName(name),
            Plate = new VehiclePlate(plate)
        };
        return customerVehicle;
    }
    public VehicleCode Code{ get; set; }
    public VehicleName Name{ get; set; }
    public VehiclePlate Plate{ get; set; }
    public VehicleColor Color{ get; set; }
}
