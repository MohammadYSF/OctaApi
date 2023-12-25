namespace Application.ReadModels;
public class SellInvoiceRM
{
    public Guid Id { get; set; }
    public Guid SellInvoiceId { get; set; }
    public string? SellInvoiceCode { get; set; }
    public DateTime SellInvoiceDate { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerCode { get; set; }
    public string? VehicleName { get; set; }
    public string? VehicleCode { get; set; }
    public long TotalPrice { get; set; }
    public long TotalPriceWhenUsingBuyPrices { get; set; }
    public long Tax { get; set; }
    public long Discount { get; set; }
    public long ToPay { get; set; }
    public long ToPayWhenUsingBuyPrices { get; set; }
}
