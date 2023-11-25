namespace OctaApi.Domain.Models;

public class Vehicle
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string Plate { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<VehicleHistory> VehicleHistory{ get; set; }
    public virtual ICollection<Invoice> Invoices{ get; set; }

}


