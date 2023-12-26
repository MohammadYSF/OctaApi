namespace Application.Core;
public class SellInvoicePaymentRM
{
    public Guid Id { get; set; }
    public Guid SellInvoiceId { get; set; }
    public long ToPayAmount { get; set; }
    public long ToPayAmountWhenUsingBuyPrice { get; set; }
    public long PaidAmount { get; set; }
}
