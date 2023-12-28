using Command.Core.Domain.Core;
using Command.Core.Domain.SellInvoice.ValueObjects;
namespace Command.Core.Domain.SellInvoice.Entities;
public class SellInvoicePayment : Entity
{
    public SellInvoicePayment()
    {

    }
    public SellInvoicePayment(Guid id, Guid sellInvoiceId, SellInvoicePaymentDate paidDate, SellInvoicePaymentTrackCode paymentTrackCode, SellInvoicePaidAmount paidAmount)
    {
        this.Id = id;
        this.SellInvoiceId = sellInvoiceId;
        this.PaidDate = paidDate;
        this.PaymentTrackCode = paymentTrackCode;
        this.PaidAmount = paidAmount;
    }
    public Guid SellInvoiceId { get; set; }
    public SellInvoicePaymentDate PaidDate { get; set; }
    public SellInvoicePaymentTrackCode PaymentTrackCode { get; set; }
    public SellInvoicePaidAmount PaidAmount { get; set; } = new SellInvoicePaidAmount(0);
}
