namespace Application.ReadModels;
public class ServicecRM
{
    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public string? ServiceCode { get; set; }
    public string? ServiceName { get; set; }
    public long ServiceDefaultPrice { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
