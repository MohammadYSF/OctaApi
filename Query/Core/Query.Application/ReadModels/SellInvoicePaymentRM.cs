namespace Query.Application.ReadModels;

public class SellInvoicePaymentRM
{
    public Guid Id { get; set; }
    public Guid SellInvoiceId { get; set; }
    public long PaidAmount { get; set; }
}
