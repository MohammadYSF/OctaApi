namespace OctaApi.Domain.Models;

public class DescriptionItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public virtual ICollection<InvoiceDescription>  InvoiceDescriptions{ get; set; }

}
