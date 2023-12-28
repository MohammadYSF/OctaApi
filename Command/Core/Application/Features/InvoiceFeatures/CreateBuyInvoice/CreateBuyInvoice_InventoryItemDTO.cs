namespace Command.Core.Application.Features.InvoiceFeatures.CreateBuyInvoice
{
    public sealed record CreateBuyInvoice_InventoryItemDTO(Guid Id, long BuyPrice, long SellPrice, float Count, float LowerBoundCount);
}
