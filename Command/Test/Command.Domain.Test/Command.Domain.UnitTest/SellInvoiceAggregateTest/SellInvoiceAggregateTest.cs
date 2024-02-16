using Command.Core.Domain.SellInvoice;
using Command.Domain.Core;
using FluentAssertions;
using OctaShared.Events;

namespace Command.Domain.UnitTest.SellInvoiceAggregateTest;

public class SellInvoiceAggregateTest
{
    [Fact]
    public void SellInvoiceAggregate_UpdateDescription_ShouldUpdateInvoiceDescription()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
            .WithId(id)
            .WithDateTime(dt)
            .WithCode(code)
            .WithCustomer(customer)
            .WithVehicle(vehicle)
            .Build();
        string newDescription = "new desc";
        sellInvoiceAggregate.UpdateDescription(newDescription);
        sellInvoiceAggregate.Description.Value.Should().Be(newDescription);
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(2);
        sellInvoiceAggregate.GetDomainEvents()[1].Should().BeOfType<SellInvoiceUpDescriptionUpdatedEvent>();
    }
    [Fact]
    public void SellInvoiceAggregate_UpdateDescription_ShouldUpdateInvoiceDescriptionWithSameOldDescription()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build(); string newDescription = string.Empty;
        sellInvoiceAggregate.UpdateDescription(newDescription);
        sellInvoiceAggregate.Description.Value.Should().Be(newDescription);
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(1);
    }
    [Fact]
    public void SellInvoiceAggregate_Create_ShouldCreateNewMiscellaneousSellInvoice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;

        var result = SellInvoiceAggregate.CreateMiscellaneous(id, dt, code);
        result.Should().NotBeNull();
        result.GetDomainEvents().Should().HaveCount(1);
        result.GetDomainEvents()[0].Should().BeOfType<SellInvoiceCreatedEvent>();
    }
    [Fact]
    public void SellInvoiceAggregate_Create_ShouldCreateNewSellInvoice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        sellInvoiceAggregate.Should().NotBeNull();
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(1);
        sellInvoiceAggregate.GetDomainEvents()[0].Should().BeOfType<SellInvoiceCreatedEvent>();
    }
    [Fact]
    public void SellInvoiceAggregate_Delete_ShouldDeleteOpenSellInvoice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        sellInvoiceAggregate.Delete();
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(2);
        sellInvoiceAggregate.GetDomainEvents()[1].Should().BeOfType<SellInvoiceDeletedEvent>();
    }
    [Fact]
    public void SellInvoiceAggregate_Delete_ShouldNotDeleteClosedSellInvoice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow.AddDays(-1);
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        Assert.Throws<DomainException<SellInvoiceAggregate>>(() => sellInvoiceAggregate.Delete());
    }
    [Fact]
    public void SellInvoiceAggregate_AddSellInvoiceInventoryItem_ShouldAddInventoryItemToInvoice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        sellInvoiceAggregate.AddSellInvoiceInventoryItem(Guid.NewGuid(), Guid.NewGuid(), 3);
        sellInvoiceAggregate.InventoryItems.Should().HaveCount(1);
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(2);
        sellInvoiceAggregate.GetDomainEvents()[1].Should().BeOfType<InventoryItemAddedToSellInvoiceEvent>();

    }
    [Fact]
    public void SellInvoiceAggregate_AddSellInvoiceService_ShouldAddServiceToInvoice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        sellInvoiceAggregate.AddSellInvoiceService(Guid.NewGuid(), Guid.NewGuid(), 3);
        sellInvoiceAggregate.Services.Should().HaveCount(1);
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(2);
        sellInvoiceAggregate.GetDomainEvents()[1].Should().BeOfType<ServiceAddedToSellInvoiceEvent>();

    }
    [Fact]
    public void SellInvoiceAggregate_RemoveSellInvoiceService_ShouldRemoveServiceToInvoice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        Guid sellInvoiceServiceId = Guid.NewGuid();
        sellInvoiceAggregate.AddSellInvoiceService(sellInvoiceServiceId, id, 3);
        int c = sellInvoiceAggregate.Services.Count;
        sellInvoiceAggregate.RemoveSellInvoiceService(sellInvoiceServiceId);
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(3);
        sellInvoiceAggregate.GetDomainEvents()[2].Should().BeOfType<ServiceRemovedFromSellInvoiceEvent>();
        sellInvoiceAggregate.Services.Should().HaveCount(c - 1);
    }
    [Fact]
    public void SellInvoiceAggregate_RemoveSellInvoiceService_ShouldRemoveServiceToInvoice_WhereGivenSellInvoiceServiceIdDoesNotExist()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        int c = sellInvoiceAggregate.Services.Count;
        sellInvoiceAggregate.RemoveSellInvoiceService(Guid.NewGuid());
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(1);
        sellInvoiceAggregate.Services.Should().HaveCount(c);
    }


    [Fact]
    public void SellInvoiceAggregate_RemoveSellInvoiceInventoryItem_ShouldRemoveInventoryItemFromInvoice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        Guid sellInvoiceInventoryItemId = Guid.NewGuid();
        sellInvoiceAggregate.AddSellInvoiceInventoryItem(sellInvoiceInventoryItemId, id, 3);
        int c = sellInvoiceAggregate.InventoryItems.Count;
        sellInvoiceAggregate.RemoveSellInvoiceInventoryItem(sellInvoiceInventoryItemId);
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(3);
        sellInvoiceAggregate.GetDomainEvents()[2].Should().BeOfType<InventoryItemRemovedFromSellInvoicecEvent>();
        sellInvoiceAggregate.InventoryItems.Should().HaveCount(c - 1);
    }
    [Fact]
    public void SellInvoiceAggregate_RemoveSellInvoiceInventoryItem_ShouldRemoveInventoryItemFromInvoice_WhereGivenSellInvoiceInventoryItemIdIdDoesNotExist()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        int c = sellInvoiceAggregate.InventoryItems.Count;
        sellInvoiceAggregate.RemoveSellInvoiceInventoryItem(Guid.NewGuid());
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(1);
        sellInvoiceAggregate.InventoryItems.Should().HaveCount(c);
    }


    [Fact]
    public void SellInvoiceAggregate_SetBuyPrice_ShouldChangeToUseBuyPrice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        sellInvoiceAggregate.UseBuyPrice.Should().BeFalse();
        sellInvoiceAggregate.SetUseBuyPrice(true);
        sellInvoiceAggregate.UseBuyPrice.Should().BeTrue();
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(2);
        sellInvoiceAggregate.GetDomainEvents()[1].Should().BeOfType<SellInvoiceUsesBuyPriceEvent>();
    }
    [Fact]
    public void SellInvoiceAggregate_SetBuyPrice_ShouldChangeToNotUseBuyPrice()
    {
        Guid id = Guid.NewGuid();
        DateTime dt = DateTime.UtcNow;
        int code = 3;
        Guid customer = Guid.NewGuid();
        Guid vehicle = Guid.NewGuid();
        var sellInvoiceAggregate = new SellInvoiceAggregateBuilder()
                    .WithId(id)
                    .WithDateTime(dt)
                    .WithCode(code)
                    .WithCustomer(customer)
                    .WithVehicle(vehicle)
                    .Build();
        sellInvoiceAggregate.SetUseBuyPrice(true);
        sellInvoiceAggregate.SetUseBuyPrice(false);
        sellInvoiceAggregate.GetDomainEvents().Should().HaveCount(3);
        sellInvoiceAggregate.GetDomainEvents()[2].Should().BeOfType<SellInvoiceDoesNotUseBuyPriceEvent>();
    }
}
