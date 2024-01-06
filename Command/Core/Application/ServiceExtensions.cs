using Command.Core.Application.Common.Behavior;
using Command.Core.Application.EventHandlers.Vehicle;
using Command.Core.Common;
using Command.Core.Domain.Customer.Events;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Command.Core.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        //services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<VehicleAggregateEventHandler>();
        services.AddTransient<IEventHandler<VehicleAddedToCustomerEvent>, VehicleAggregateEventHandler>();

    }
}
