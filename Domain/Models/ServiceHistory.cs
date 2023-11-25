namespace OctaApi.Domain.Models;

public class ServiceHistory
{
    public Guid Id { get; set; }
    public Guid ServiceId{ get; set; }
    public string Name { get; set; }
    public long DefaultPrice { get; set; } //new
    public DateTime UpdateDate { get; set; }
    public bool IsActive { get; set; }
    public virtual Service Service { get; set; }

}


