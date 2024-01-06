using Command.Core.Common;
using Command.Core.Domain.Core;
namespace Command.Core.Application.Repositories;
public interface IEventBus
{
    void Publish<T>(T @event) where T : DomainEvent;
    void Subscribe<T, TH>() where T : DomainEvent where TH : IEventHandler<T>;
}
