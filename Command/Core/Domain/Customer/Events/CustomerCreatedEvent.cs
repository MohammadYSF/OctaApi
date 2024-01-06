using Command.Core.Domain.Core;

namespace Command.Core.Domain.Customer.Events;
public class CustomerCreatedEvent : DomainEvent
{
    public CustomerCreatedEvent() : base(nameof(CustomerCreatedEvent))
    {
    }
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public int Code { get; set; }
}
