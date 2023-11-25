namespace OctaApi.Domain.Models;

public class CustomerHistory
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public int Code { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsActive { get; set; }
    public virtual Customer Customer { get; set; }
}


