using Command.Core.Application.Repositories;
using Command.Infrastructure.RabbitMqBus.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Command.Infrastructure.RabbitMqBus;
public static class ServiceExtentions
{
    public static void ConfigureBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfig>(configuration.GetSection(nameof(RabbitMqConfig)));
        services.AddSingleton<IEventBus, RabbitMQBus>();
    }
}
