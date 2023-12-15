using Domain.Customer.Entities;
using Domain.Customer.ValueObjects;
using OctaApi.Domain;
using OctaApi.Domain.Models;
namespace Domain.Customer;
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
        return customerAggregate;
    }
    public void AddVehicle(Guid id, int vehicleCode, string vehicleName, string vehicleColor, string vehiclePlate)
    {
        var vehicle = CustomerVehicle.Create(id, vehicleCode, vehicleName, vehicleColor, vehiclePlate);
        this.CustomerVehicles.Add(vehicle);
    }
    public void RemoveVehicle(Guid id)
    {
        this.CustomerVehicles = this.CustomerVehicles.Where(a => a.Id != id).ToList();
    }
    public CustomerCode Code { get; set; }
    public CustomerFirstName FirstName { get; set; }
    public CustomerLastName LastName { get; set; }
    public CustomerPhoneNumber PhoneNumber { get; set; }
    public List<CustomerVehicle> CustomerVehicles { get; set; }
}
