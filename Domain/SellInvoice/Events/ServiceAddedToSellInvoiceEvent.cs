using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SellInvoice.Events;

public class ServiceAddedToSellInvoiceEvent:DomainEvent
{
    public Guid SellInvoiceServiceId { get; set; }
    public Guid SellInvoiceId { get; set; }
    public Guid ServiceId { get; set; }
    public long Price{ get; set; }
}
