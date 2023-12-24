namespace Application.ReadModels;
public class InventoryItemRM
{
    public Guid Id { get; set; }
    public Guid InventoryItemId { get; set; }
    public string? InventoryItemCode { get; set; }
    public string? InventoryItemName { get; set; }
    public float InventoryItemCount { get; set; }
    public long InventoryItemBuyPrice { get; set; }
    public long InventoryItemSellPrice { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
