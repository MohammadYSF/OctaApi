using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Query.Application.Repositories;
using Query.Infrastructure.RabbitMqBus.Services;
namespace Query.Infrastructure.RabbitMqBus;
public static class ServiceExtentions
{
    public static void ConfigureBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfig>(configuration.GetSection(nameof(RabbitMqConfig)));
        services.AddSingleton<IEventBus, RabbitMQBus>();
    }
}
