namespace Query.Application.ReadModels;

public class SellInvoiceDescriptionRM
{
    public Guid Id { get; set; }
    public Guid SellInvoiceId { get; set; }
    public long Kilometer1 { get; set; }
    public long Kilometer2 { get; set; }
    public long Kilometer3 { get; set; }
    public long Kilometer4 { get; set; }
    public string Description { get; set; }
}
