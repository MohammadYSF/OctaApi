using Microsoft.Extensions.DependencyInjection;
using OctaShared.Contracts;
using OctaShared.Events;
using OctaShared.Events.Events.InventoryItem;
using Query.Application.EventHandlers.BuyInvoice;
using Query.Application.EventHandlers.Customer;
using Query.Application.EventHandlers.InventoryItem;
using Query.Application.EventHandlers.SellInvoice;
using Query.Application.EventHandlers.Service;
using System.Reflection;

namespace Query.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddTransient<BuyInvoiceEventHandler>();
        services.AddTransient<IEventHandler<BuyInvoiceCreatedEvent>, BuyInvoiceEventHandler>();


        services.AddTransient<CustomerEventHandler>();
        services.AddTransient<IEventHandler<CustomerCreatedEvent>, CustomerEventHandler>();
        services.AddTransient<IEventHandler<VehicleCreatedEvent>, CustomerEventHandler>();

        services.AddTransient<InventoryItemEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemCreatedEvent>, InventoryItemEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemUpdatedEvent>, InventoryItemEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemBoughtEvent>, InventoryItemEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemRemovedFromSellInvoicecEvent>, InventoryItemEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemAddedToSellInvoiceEvent>, InventoryItemEventHandler>();
        services.AddTransient<IEventHandler<InventoryItemDeletedEvent>, InventoryItemEventHandler>();

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
