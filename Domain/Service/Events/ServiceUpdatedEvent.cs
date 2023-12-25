using Domain.BuyInvoice.Events;
using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Events;

public class ServiceUpdatedEvent:DomainEvent
{
    public ServiceUpdatedEvent() : base(nameof(ServiceUpdatedEvent))
    {

    }
    public Guid ServiceId { get; set; }
    public string NewName{ get; set; }
    public long NewDefaultPrice{ get; set; }
    public DateTime UpdateDate{ get; set; }
    public int Code { get; set; }
}
