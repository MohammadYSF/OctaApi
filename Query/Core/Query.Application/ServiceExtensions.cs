using Microsoft.Extensions.DependencyInjection;
using Query.Application.Core;
using Query.Application.EventHandlers.BuyInvoice;
using Query.Application.EventHandlers.Customer;
using Query.Application.EventHandlers.InventoryItem;
using Query.Application.EventHandlers.SellInvoice;
using Query.Application.EventHandlers.Service;
using Query.Application.Events.BuyInvoice;
using Query.Application.Events.Customer;
using Query.Application.Events.InventoryItem;
using Query.Application.Events.SellInvoice;
using Query.Application.Events.Services;
using Query.Application.Events.Vehicles;

namespace Query.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddTransient<BuyInvoiceEventHandler>();
        services.AddTransient<IEventHandler<BuyInvoiceCreatedEvent>, BuyInvoiceEventHandler>();
        services.AddTransient<CustomerEventHandler>();
        services.AddTransient<IEventHandler<CustomerCreatedEvent>, CustomerEventHandler>();
        services.AddTransient<IEventHandler<VehicleCreatedEvent>, CustomerEventHandler>();

        services.AddTransient<InventoryItemEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemCreatedEvent>, InventoryItemEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemUpdatedEvent>, InventoryItemEventHandler>();

        services.AddTransient<SellInvoiceEventHandler>();
        services.AddTransient<IEventHandler<SellInvoiceCreatedEvent>, SellInvoiceEventHandler>();
        services.AddTransient<IEventHandler<SellInvoiceDeletedEvent>, SellInvoiceEventHandler>();
        services.AddTransient<IEventHandler<ServiceAddedToSellInvoiceEvent>, SellInvoiceEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemAddedToSellInvoiceEvent>, SellInvoiceEventHandler>();
        services.AddTransient<IEventHandler<ServiceRemovedFromSellInvoiceEvent>, SellInvoiceEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemRemovedFromSellInvoicecEvent>, SellInvoiceEventHandler>();
        services.AddTransient<IEventHandler<SellInvoicePaymentCreatedEvent>, SellInvoiceEventHandler>();

        services.AddTransient<ServiceEventHandler>();
        services.AddTransient<IEventHandler<ServiceCreatedEvent>, ServiceEventHandler>();
        services.AddTransient<IEventHandler<ServiceUpdatedEvent>, ServiceEventHandler>();

    }
}
