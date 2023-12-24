using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BuyInvoice.Events;

public class BuyInvoiceCreatedEvent : DomainEvent
{
    public BuyInvoiceCreatedEvent() : base(nameof(BuyInvoiceCreatedEvent))
    {

    }

    public Guid BuyInvoiced { get; set; }
    public DateTime BuyDate { get; set; }
    public string Code { get; set; }
    public string SellerName { get; set; }
    public long TotalPrice { get; set; }
}
