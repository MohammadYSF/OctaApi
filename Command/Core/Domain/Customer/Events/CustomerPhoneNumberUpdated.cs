using Command.Core.Domain.Core;

namespace Command.Core.Domain.Customer.Events;

public class CustomerPhoneNumberUpdated : DomainEvent
{
    public CustomerPhoneNumberUpdated() : base(nameof(CustomerPhoneNumberUpdated))
    {

    }
    public Guid CustomerId { get; set; }
    public string NewPhoneNumber { get; set; }
}
