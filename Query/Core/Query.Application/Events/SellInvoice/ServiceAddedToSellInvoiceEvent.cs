﻿using Query.Application.Core;
namespace Query.Application.Events.SellInvoice;
public class ServiceAddedToSellInvoiceEvent : DomainEvent
{
    public ServiceAddedToSellInvoiceEvent() : base(nameof(ServiceAddedToSellInvoiceEvent))
    {
    }
    public Guid SellInvoiceServiceId { get; set; }
    public Guid SellInvoiceId { get; set; }
    public Guid ServiceId { get; set; }
    public long Price { get; set; }
}
