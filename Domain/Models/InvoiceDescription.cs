namespace OctaApi.Domain.Models;

public class InvoiceDescription
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid DescriptionItemId { get; set; }
    public string Value{ get; set; }
    public virtual Invoice Invoice { get; set; }
    public virtual DescriptionItem DescriptionItem { get; set; }
}