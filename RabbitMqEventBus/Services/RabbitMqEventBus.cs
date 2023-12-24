using Application.Repositories;
using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Infrastructure.RabbitMqBus.Services;

public class RabbitMqEventBus : IEventBus
{
    public Task PublishAsync(DomainEvent @event)
    {
        Console.WriteLine("an event raised");
        return Task.CompletedTask;
    }
}
