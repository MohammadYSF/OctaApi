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
    public void AddVehicle(Guid vehicleId)
    {
        this.Vehicles.Add(vehicleId);
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
