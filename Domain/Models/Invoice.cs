using OctaApi.Domain.Enums;

namespace OctaApi.Domain.Models;

public class Invoice
{
    public Guid Id { get; set; }
    public Guid? VehicleId { get; set; }
    public Guid? CustomerId { get; set; }
    public float? DiscountAmount{ get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? SerllerName { get; set; } //new
    public InvoiceType Type{ get; set; } //new
    public bool? UseBuyPrice { get; set; } //new
    public int Code{ get; set; } //new
    public DateTime RegisterDate { get; set; }
    public virtual ICollection<InvoiceDescription>  InvoiceDescriptions { get; set; }
    public virtual ICollection<InvoiceInventoryItem> InvoiceInventoryItems{ get; set; }
    public virtual ICollection<InvoicePayment>  InvoicePayments{ get; set; }
    public virtual ICollection<InvoiceService> InvoiceServices{ get; set; }
    public virtual Vehicle? Vehicle { get; set; }
    public virtual Customer? Customer{ get; set; }

    public string? Description { get; set; }


}
