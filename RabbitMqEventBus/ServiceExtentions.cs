using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OctaApi.Infrastructure.RabbitMqBus.Services;
using RabbitMqBus;
namespace OctaApi.Infrastructure.RabbitMqBus;
public static class ServiceExtentions
{
    public static void ConfigureBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfig>(configuration.GetSection(nameof(RabbitMqConfig)));
        services.ConfigureOptions<RabbitMqConfig>();
        services.AddSingleton<IEventBus, RabbitMQBus>();
    }
}
