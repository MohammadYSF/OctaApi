namespace Application.ReadModels;
public class DailyInvoiceRM
{
    public Guid Id { get; set; }
    public Guid SellInvoiceId { get; set; }
    public string? SellInvoiceCode { get; set; }
    public string? CustomerName { get; set; }
    public string? VehicleName { get; set; }
    public long TotalPrice { get; set; }
}
