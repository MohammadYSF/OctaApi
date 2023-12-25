﻿using Domain.BuyInvoice.Events;
using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Events;

public class ServiceCreatedEvent:DomainEvent
{
    public ServiceCreatedEvent() : base(nameof(ServiceCreatedEvent))
    {

    }
    public Guid ServiceId { get; set; }
    public string Name{ get; set; }
    public long DefaultPrice{ get; set; }
    public int Code { get; set; }
    public DateTime CreateDateTime { get; set; }
}