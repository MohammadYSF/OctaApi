using OctaApi.Domain.Common;
namespace Domain.Customer.Events;
public class CustomerCreatedEvent:DomainEvent
{
    public CustomerCreatedEvent() : base(nameof(CustomerCreatedEvent))
    {

    }
    public Guid CustomerId { get; set; }
    public string FirstName{ get; set; }
    public string LastName{ get; set; }
    public int Code{ get; set; }
}
