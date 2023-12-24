using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SellInvoice.Events;

public class SellInvoiceUpDescriptionUpdatedEvent:DomainEvent
{
    public SellInvoiceUpDescriptionUpdatedEvent() : base(nameof(SellInvoiceUpDescriptionUpdatedEvent))
    {

    }
    public Guid SellInvoiceId { get; set; }
    public string NewDescription{ get; set; }
}
