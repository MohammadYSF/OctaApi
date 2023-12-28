
using Command.Core.Domain.Core;

namespace Command.Core.Common;

public interface IEventHandler<T> where T : DomainEvent
{
    Task HandleAsync(T @event);
}
