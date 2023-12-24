﻿using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SellInvoice.Events;

public class SellInvoiceUsesBuyPriceEvent : DomainEvent
{
    public SellInvoiceUsesBuyPriceEvent() : base(nameof(SellInvoiceUsesBuyPriceEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
}
