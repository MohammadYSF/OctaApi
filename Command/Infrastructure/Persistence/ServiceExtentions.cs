using Command.Core.Application.Repositories;
using Command.Infrastructure.Persistence.Contexts;
using Command.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Command.Infrastructure.Persistence.Persistence;
public static class ServiceExtentions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("OAS");
        services.AddDbContext<WriteDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
        services.AddScoped<IVehicleCommandRepository, VehicleCommandRepository>();
        services.AddScoped<IInventoryItemCommandRepository, InventoryItemCommandRepository>();
        services.AddScoped<IServiceCommandRepository, ServiceCommandRepository>();
        services.AddScoped<IBuyInvoiceCommandRepository, BuyInvoiceCommandRepository>();
        services.AddScoped<ISellInvoiceCommandRepository, SellInvoiceCommandRepository>();
        services.AddScoped<ICommandUnitOfWork, CommandUnitOfWork>();
    }
}
