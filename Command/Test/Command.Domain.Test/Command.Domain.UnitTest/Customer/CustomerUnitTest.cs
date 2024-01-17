using Command.Core.Domain.Customer;
using FluentAssertions;
using Moq;
using OctaShared.Events;

namespace Command.Domain.UnitTest.Customer;

public class CustomerUnitTest
{
    [Fact]
    public void CustomerAggregate_Create_ShouldCreateNewCustomer()
    {
        Guid id = Guid.NewGuid();
        int code = It.IsAny<int>();
        string firstName = It.IsAny<string>();
        string lastName = It.IsAny<string>();
        string phone = It.IsAny<string>();
        var customerAggregate = CustomerAggregate.Create(id, code, firstName, lastName, phone);
        customerAggregate.Should().NotBeNull();
        customerAggregate.GetDomainEvents().Should().HaveCount(1);
        customerAggregate.GetDomainEvents()[0].Should().BeOfType<CustomerCreatedEvent>();
    }
    [Fact]
    public void CustomerAggregate_AddVehicle_ShouldAddVehicleToCustomer()
    {
        Guid vehicleId = Guid.NewGuid();
        string vehicleName = It.IsAny<string>();
        string vehicleColor = It.IsAny<string>();
        string vehiclePlate = It.IsAny<string>();
        var customerAggregate = new CustomerAggregate();
        customerAggregate.AddVehicle(vehicleId, vehicleName, vehiclePlate, vehicleColor);
        customerAggregate.Should().NotBeNull();
        customerAggregate.Vehicles.Should().HaveCount(1);
        customerAggregate.GetDomainEvents().Should().HaveCount(1);
        customerAggregate.GetDomainEvents()[0].Should().BeOfType<VehicleAddedToCustomerEvent>();

    }
    [Fact]
    public void CustomerAggregate_RemoveVehicle_ShouldRemoveVehicleFromCustomer()
    {
        Guid vehicleId = Guid.NewGuid();
        var customerAggregate = new CustomerAggregate();
        customerAggregate.AddVehicle(vehicleId, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        customerAggregate.RemoveVehicle(vehicleId);
        customerAggregate.Vehicles.Should().HaveCount(0);
    }
    [Fact]
    public void CustomerAggregate_UpdateCustomerPhoneNumber_ShouldUpdateCustomerPhoneNumber()
    {
        string phoneNumber = "1111111";
        var customerAggregate = new CustomerAggregate();
        customerAggregate.UpdateCustomerPhoneNumber(phoneNumber);
        customerAggregate.PhoneNumber.Value.Should().Be(phoneNumber);
    }
}