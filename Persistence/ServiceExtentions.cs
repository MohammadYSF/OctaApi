using Application.Repositories;
using Application.Repositories.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OctaApi.Application.Repositories;
using OctaApi.Persistence.Contexts;
using OctaApi.Persistence.Repositories;
using Persistence.Repositories;
namespace OctaApi.Persistence;
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
