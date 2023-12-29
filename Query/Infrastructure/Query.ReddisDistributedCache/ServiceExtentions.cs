using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Query.Application.ReadModels;
using Query.Application.Repositories;

namespace Query.Infrastructure.RedisDistributedCache;
public static class ServiceExtentions
{
    public static void ConfigureCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisConfig>(configuration.GetSection(nameof(RedisConfig)));
        services.AddStackExchangeRedisCache(options => { options.Configuration = configuration[nameof(RedisConfig)]; });
        services.AddSingleton<IDistributedCacheService<VehicleRM>, RedisDistributedCacheService<VehicleRM>>();
        services.AddSingleton<IDistributedCacheService<CustomerRM>, RedisDistributedCacheService<CustomerRM>>();
        services.AddSingleton<IDistributedCacheService<ServiceRM>, RedisDistributedCacheService<ServiceRM>>();
        services.AddSingleton<IDistributedCacheService<InventoryItemRM>, RedisDistributedCacheService<InventoryItemRM>>();
        services.AddSingleton<IDistributedCacheService<SellInvoiceRM>, RedisDistributedCacheService<SellInvoiceRM>>();
        services.AddSingleton<IDistributedCacheService<BuyInvoiceRM>, RedisDistributedCacheService<BuyInvoiceRM>>();
        services.AddSingleton<IDistributedCacheService<CustomerVehicleRM>, RedisDistributedCacheService<CustomerVehicleRM>>();
        services.AddSingleton<IDistributedCacheService<SellInvoiceDescriptionRM>, RedisDistributedCacheService<SellInvoiceDescriptionRM>>();
        services.AddSingleton<IDistributedCacheService<SellInvoiceServiceRM>, RedisDistributedCacheService<SellInvoiceServiceRM>>();
        services.AddSingleton<IDistributedCacheService<SellInvoiceInventoryItemRM>, RedisDistributedCacheService<SellInvoiceInventoryItemRM>>();
        services.AddSingleton<IDistributedCacheService<SellInvoicePaymentRM>, RedisDistributedCacheService<SellInvoicePaymentRM>>();

    }
}
