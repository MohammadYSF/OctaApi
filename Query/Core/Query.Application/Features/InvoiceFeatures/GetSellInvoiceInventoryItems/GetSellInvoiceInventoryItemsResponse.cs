using Query.Application.ReadModels;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed record GetSellInvoiceInventoryItemsResponse(List<SellInvoiceInventoryItemRM> Data);
}
