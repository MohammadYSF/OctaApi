namespace OctaApi.Domain.Models;

public class InvoiceService
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid ServiceId { get; set; }
    public long Price { get; set; } //new
    public virtual Invoice Invoice { get; set; }
    public virtual Service Service { get; set; }
    public DateTime RegisterDate { get; set; }

}