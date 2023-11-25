namespace OctaApi.Domain.Models;

public class InvoicePayment
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public long PaidAmount{ get; set; }
    public string? TrackCode { get; set; }
    public DateTime LastPaymentDate { get; set; }
    public virtual Invoice Invoice { get; set; }
    public virtual ICollection<InvoicePaymentHistory> InvoicePaymentHistories{ get; set; }

}
