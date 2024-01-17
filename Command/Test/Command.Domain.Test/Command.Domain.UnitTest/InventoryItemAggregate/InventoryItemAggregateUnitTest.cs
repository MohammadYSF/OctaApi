using Command.Core.Domain.InventoryItem;
using Command.Core.Domain.InventoryItem.ValueObjects;
using FluentAssertions;
using Moq;
using OctaShared.Events;

namespace Command.Domain.UnitTest.InventoryItemAggregate;

public class InventoryItemAggregateUnitTest
{
    [Fact]
    public void InventoryItemAggregate_Create_ShouldCreateNewInventoryItem()
    {
        var id = It.IsAny<Guid>();
        var name = It.IsAny<string>();
        var code = It.IsAny<int>();
        var inventoryItemAggregate = InventoryItemَAggregate.Create(id, name, code);
        inventoryItemAggregate.Should().NotBeNull();
        inventoryItemAggregate.Id.Should().Be(id);
        inventoryItemAggregate.Name.Value.Should().Be(name);
        inventoryItemAggregate.Code.Value.Should().Be(code);
        inventoryItemAggregate.GetDomainEvents().Should().HaveCount(1);
        inventoryItemAggregate.GetDomainEvents()[0].Should().BeOfType<InventoryItemCreatedEvent>();
    }
    [Fact]
    public void InventoryItemAggregate_Delete_ShouldDeleteInventoryItemAggregate()
    {
        var inventoryItemAggregate = new InventoryItemَAggregate();
        inventoryItemAggregate.Delete();
        inventoryItemAggregate.IsActive.Should().BeFalse();
    }
    [Fact]

    public void InventoryItemAggregate_Buy_ShouldBuy()
    {
        var inventoryItemAggregate = new InventoryItemَAggregate();
        inventoryItemAggregate.Name = new InventoryItemName("test");
        inventoryItemAggregate.Code = new InventoryItemCode(10);
        inventoryItemAggregate.Buy(3, 4, 3);
        inventoryItemAggregate.Should().NotBeNull();
        inventoryItemAggregate.GetDomainEvents().Should().HaveCount(1);
        inventoryItemAggregate.GetDomainEvents()[0].Should().BeOfType<InventoryItemBoughtEvent>();
    }
    [Fact]

    public void InventoryItemAggregate_Use_ShouldDecreaseCount()
    {
        var inventoryItemAggregate = InventoryItemَAggregate.Create(Guid.NewGuid(), "test", 10);
        inventoryItemAggregate.Buy(3, 10, 100);
        inventoryItemAggregate.Use(8);
        inventoryItemAggregate.Count.Value.Should().Be(100 - 8);
    }
}
