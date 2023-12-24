namespace Application.ReadModels;

public class OpenInvoiceServiceRM
{
    public Guid Id { get; set; }
    public Guid SellInvoiceId { get; set; }
    public Guid ServiceId { get; set; }
    public string? ServiceCode { get; set; }
    public long DefaultPrice { get; set; }
    public long Price { get; set; }
}