using Command.Core.Domain.Vehicle;
using FluentAssertions;

namespace Command.Domain.UnitTest.VehicleAggregateTest;

public class VehicleAggregateUnitTest
{
    [Fact]
    public void VehicleAggregate_Create_ShouldCreateNewVehicle()
    {
        Guid id = Guid.NewGuid();
        int code = 20;
        string name = "vehicleA";
        string color = "red";
        string plate = "23D123";
        var serviceAggregate = VehicleAggregate.Create(id, code, name, color, plate);
        serviceAggregate.Should().NotBeNull();

    }
}
