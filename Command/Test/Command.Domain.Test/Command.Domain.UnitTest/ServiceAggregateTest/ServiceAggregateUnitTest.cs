using Command.Core.Domain.Service;
using Command.Core.Domain.Service.Events;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command.Domain.UnitTest.ServiceAggregateTest;

public class ServiceAggregateUnitTest
{
    [Fact]
    public void ServiceAggregate_Create_ShouldCreateNewService()
    {
        Guid id = Guid.NewGuid();
        string name = "ServiceA";
        int code = 10;
        long defaultPrice = 1000;
        var serviceAggregate = ServiceAggregate.Create(id, name, code, defaultPrice);
        serviceAggregate.Should().NotBeNull();
        serviceAggregate.GetDomainEvents().Should().HaveCount(1);
        serviceAggregate.GetDomainEvents()[0].Should().BeOfType<ServiceCreatedEvent>();
    }

    [Fact]
    public void ServiceAggregate_Delete_ShouldDeleteService()
    {
        Guid id = Guid.NewGuid();
        string name = "ServiceA";
        int code = 10;
        long defaultPrice = 1000;
        var serviceAggregate = ServiceAggregate.Create(id, name, code, defaultPrice);
        serviceAggregate.IsActive.Should().BeTrue();
        serviceAggregate.Delete();
        serviceAggregate.IsActive.Should().BeFalse();

    }
}
