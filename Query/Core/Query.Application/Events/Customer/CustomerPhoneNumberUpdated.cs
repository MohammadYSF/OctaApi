using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Events.Customer;

public class CustomerPhoneNumberUpdated:DomainEvent
{
    public CustomerPhoneNumberUpdated() : base(nameof(CustomerPhoneNumberUpdated))
    {

    }
    public Guid CustomerId { get; set; }
    public string NewPhoneNumber{ get; set; }
}
