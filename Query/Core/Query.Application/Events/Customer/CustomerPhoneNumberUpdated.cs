using Query.Application.Core;

namespace Query.Application.Events.Customer;

public class CustomerPhoneNumberUpdated : DomainEvent
{
    public CustomerPhoneNumberUpdated() : base(nameof(CustomerPhoneNumberUpdated))
    {

    }
    public Guid CustomerId { get; set; }
    public string NewPhoneNumber { get; set; }
}
