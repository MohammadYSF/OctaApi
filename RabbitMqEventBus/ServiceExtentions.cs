using Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using OctaApi.Infrastructure.RabbitMqBus.Services;

namespace OctaApi.Infrastructure.RabbitMqBus;

public static class ServiceExtentions
{
    public static void ConfigurePersistence(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, RabbitMqEventBus>();

    }
}
