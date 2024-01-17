using Command.Core.Domain.Invoice;
using Command.Core.Domain.BuyInvoice;
using Command.Core.Domain.SellInvoice;
using Command.Core.Domain.BuyInvoice.Entities;
using FluentAssertions;
namespace Command.Domain.UnitTest.BuyInvoiceAggregateTest;

public class BuyInvoiceAggregateUnitTest
{
    [Fact]
    public void BuyInvoiceAggregate_Create_ShouldCreateNewBuyInvoice_With0InventoryItems()
    {
        Guid id = Guid.NewGuid();
        DateTime buyDate = DateTime.UtcNow;
        int code = 2;
        string sellerName = "najafikhah";
        List<BuyInvoiceInventoryItem> buyInvoiceInventoryItems = new();
        var result = BuyInvoiceAggregate.Create(id, buyDate, code, sellerName, buyInvoiceInventoryItems);
        result.Should().NotBeNull();
    }
    [Fact]

    public void BuyInvoiceAggregate_Create_ShouldCreateNewBuyInvoice_With1OrMoreInventoryItem()
    {
        Guid id = Guid.NewGuid();
        DateTime buyDate = DateTime.UtcNow;
        int code = 2;
        string sellerName = "najafikhah";
        List<BuyInvoiceInventoryItem> buyInvoiceInventoryItems = new()
        {
            new BuyInvoiceInventoryItem
            {
                InventoryItemId =Guid.NewGuid(),
                BuyInvoiceId= id,
                Count=2
            }
    };
    var result = BuyInvoiceAggregate.Create(id, buyDate, code, sellerName, buyInvoiceInventoryItems);
    result.Should().NotBeNull();
}
}
