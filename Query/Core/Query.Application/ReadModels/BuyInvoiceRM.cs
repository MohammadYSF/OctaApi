namespace Query.Application.ReadModels;
public class BuyInvoiceRM
{
    public Guid Id { get; set; }
    public Guid BuyInvoiceId { get; set; }
    public string BuyInvoiceCode { get; set; }
    public DateTime BuyInvoiceCreateDate{ get; set; }
    public long TotalPrice { get; set; }
}
