namespace OctaApi.Domain.Models;

public class VehicleHistory
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string Plate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsActive { get; set; }
    public virtual Vehicle Vehicle { get; set; }

}


