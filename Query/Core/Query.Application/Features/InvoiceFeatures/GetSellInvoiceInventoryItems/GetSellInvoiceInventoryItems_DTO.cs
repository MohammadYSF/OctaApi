namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed record GetSellInvoiceInventoryItems_DTO(Guid InventoryItemId,Guid InvoiceInventoryItemId,string InventoryItemCode,int RowNumber, string InventoryItemName, float InventoryItemCount, long UnitBuyPrice,long UnitSellPrice, float TotalPrice);
}
