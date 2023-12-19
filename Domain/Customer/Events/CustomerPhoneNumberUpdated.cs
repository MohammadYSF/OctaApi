using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customer.Events;

public class CustomerPhoneNumberUpdated:DomainEvent
{
    public Guid CustomerId { get; set; }
    public string NewPhoneNumber{ get; set; }
}
