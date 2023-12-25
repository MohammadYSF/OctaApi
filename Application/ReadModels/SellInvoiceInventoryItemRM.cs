namespace Application.ReadModels;
public class SellInvoiceInventoryItemRM
{
    public Guid Id { get; set; }
    public Guid SellInvoiceId { get; set; }
    public Guid InventoryItemId { get; set; }
    public string? InventoryItemCode { get; set; }
    public float Count { get; set; }
    public long BuyPrice { get; set; }
    public long SellPrice { get; set; }
}
