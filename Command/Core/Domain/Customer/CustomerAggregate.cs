using Command.Core.Domain.Core;
using Command.Core.Domain.Customer.Events;
using Command.Core.Domain.Customer.ValueObjects;
namespace Command.Core.Domain.Customer;
public class CustomerAggregate : AggregateRoot
{
    public static CustomerAggregate Create(
        Guid id,
        int code,
        string firstName,
        string lastName,
        string phone
        )
    {
        var customerAggregate = new CustomerAggregate
        {
            Id = id,
            Code = new CustomerCode(code),
            FirstName = new CustomerFirstName(firstName),
            LastName = new CustomerLastName(lastName),
            PhoneNumber = new CustomerPhoneNumber(phone)
        };
        customerAggregate.AddDomainEvent(new CustomerCreatedEvent
        {
            Code = code,
            CustomerId = id,
            EventId = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,

        });
        return customerAggregate;
    }
    public void AddVehicle(Guid vehicleId, string vehicleName, string vehiclePlate, string vehicleColor)
    {
        this.Vehicles.Add(vehicleId);
        var vehicleCraetedEvent = new VehicleAddedToCustomerEvent
        {
            VehicleColor = vehicleColor,
            VehicleName = vehicleName,
            VehiclePlate = vehiclePlate,
            EventId = Guid.NewGuid(),
            VehicleId = vehicleId,
            CustomerId = this.Id,
        };
        this.AddDomainEvent(vehicleCraetedEvent);
    }
    public void RemoveVehicle(Guid vehicleId)
    {
        this.Vehicles = this.Vehicles.Where(a => a != vehicleId).ToList();
    }
    public void UpdateCustomerPhoneNumber(string phoneNumber)
    {
        this.PhoneNumber = new CustomerPhoneNumber(phoneNumber);
    }
    public CustomerCode Code { get; set; }
    public CustomerFirstName FirstName { get; set; }
    public CustomerLastName LastName { get; set; }
    public CustomerPhoneNumber PhoneNumber { get; set; }
    public List<Guid> Vehicles { get; set; } = new();
}
