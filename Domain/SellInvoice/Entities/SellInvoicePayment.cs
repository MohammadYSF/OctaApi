using Domain.SellInvoice.ValueObjects;
using OctaApi.Domain.Common;
namespace Domain.SellInvoice.Entities;
public class SellInvoicePayment:Entity
{
    public SellInvoicePayment(Guid id , Guid sellInvoiceId,SellInvoicePaymentDate paidDate,SellInvoicePaymentTrackCode trackCode,SellInvoicePaidAmount sellInvoicePaidAmount)
    {
        this.Id = id;
        this.SellInvoiceId = sellInvoiceId;
        this.PaidDate = paidDate;
        this.PaymentTrackCode = trackCode;
        this.PaidAmount = sellInvoicePaidAmount;
    }
    public Guid SellInvoiceId { get; set; }
    public SellInvoicePaymentDate PaidDate{ get; set; }
    public SellInvoicePaymentTrackCode PaymentTrackCode{ get; set; }
    public SellInvoicePaidAmount PaidAmount { get; set; } = new SellInvoicePaidAmount(0);
}
