using Domain.SellInvoice.ValueObjects;
using OctaApi.Domain.Common;
namespace Domain.SellInvoice.Entities;
public class SellInvoicePayment:Entity
{
    public SellInvoicePaymentDate PaidDate{ get; set; }
    public SellInvoicePaymentTrackCode PaymentTrackCode{ get; set; }
    public SellInvoicePaidAmount PaidAmount{ get; set; }
}
