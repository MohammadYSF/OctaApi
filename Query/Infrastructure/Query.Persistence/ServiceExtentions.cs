using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Query.Application.Repositories;
using Query.Persistence.Contexts;
using Query.Persistence.Repositories;

namespace Query.Persistence;
public static class ServiceExtentions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("OAS_Query");
        services.AddDbContext<QueryDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
        services.AddScoped<IVehicleQueryRepository, VehicleQueryRepository>();
        services.AddScoped<IBuyInvoiceQueryRepository, BuyInvoiceQueryRepository>();
        services.AddScoped<ISellInvoiceQueryRepository, SellInvoiceQueryRepository>();
        services.AddScoped<IInventoryItemQueryRepository, InventoryItemQueryRepository>();
        services.AddScoped<IServiceQueryRepository, ServiceQueryRepository>();
        services.AddScoped<IQueryUnitOfWork, QueryUnitOfWork>();
    }
}
