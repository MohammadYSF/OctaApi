﻿using Domain.Core;
using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SellInvoice.Entities;

public class SellInvoiceService:Entity
{
    public SellInvoiceService()
    {
        
    }
    public SellInvoiceService(Guid id , Guid sellInvoiceId , Guid serviceId , Price servicePrice)
    {
        this.Id = id;
        this.ServicePrice = servicePrice;
        this.ServiceId = serviceId;
        this.SellInvoiceId = sellInvoiceId;
    }
    public Guid SellInvoiceId { get; set; }
    public Guid ServiceId { get; set; }
    public Price ServicePrice { get; set; } = new Price(0);
}
