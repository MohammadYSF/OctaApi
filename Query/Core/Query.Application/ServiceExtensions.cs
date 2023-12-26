using Microsoft.Extensions.DependencyInjection;
using Query.Application.EventHandlers.BuyInvoice;
using Query.Application.EventHandlers.Customer;
using Query.Application.EventHandlers.InventoryItem;
using Query.Application.EventHandlers.SellInvoice;
using Query.Application.EventHandlers.Service;

namespace Query.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddTransient<BuyInvoiceEventHandler>();
        services.AddTransient<CustomerEventHandler>();
        services.AddTransient<InventoryItemEventHandler>();
        services.AddTransient<SellInvoiceEventHandler>();
        services.AddTransient<ServiceEventHandler>();
    }
}
