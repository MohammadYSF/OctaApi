﻿using Domain.Service.Events;
using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SellInvoice.Events;

public class ServiceRemovedFromSellInvoiceEvent : DomainEvent
{
    public ServiceRemovedFromSellInvoiceEvent() : base(nameof(ServiceRemovedFromSellInvoiceEvent))
    {

    }
    public Guid SellInvoiceServiceId { get; set; }
    public Guid SellInvoiceId { get; set; }
}
