﻿using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customer.Events;

public class VehicleAddedToCustomerEvent:DomainEvent
{
    public VehicleAddedToCustomerEvent() : base(nameof(VehicleAddedToCustomerEvent))
    {

    }
    public Guid CustomerId { get; set; }
    public Guid VehicleId { get; set; }

}