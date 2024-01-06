using Query.Application.ReadModels;
namespace Query.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo;
public sealed record GetInvoicePaymentInfoResponse
{
    public List<SellInvoicePaymentRM> SellInvoicePaymentRMs { get; set; }
    public VehicleRM VehicleRM { get; set; }
    public CustomerRM CustomerRM { get; set; }
    public SellInvoiceRM SellInvoiceRM { get; set; }
}
