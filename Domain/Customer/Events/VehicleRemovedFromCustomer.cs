using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customer.Events;

public class VehicleRemovedFromCustomer:DomainEvent
{
    public Guid VehicleId { get; set; }
    public Guid CustomerId { get; set; }
}
