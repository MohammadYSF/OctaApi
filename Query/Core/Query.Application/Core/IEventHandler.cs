using OctaApi.Domain.Common;

namespace Application.Common;

public interface IEventHandler<T> where T : DomainEvent
{
    Task HandleAsync(T @event,CancellationToken cancellationToken);
}
