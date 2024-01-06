namespace Query.Application.Core;


public interface IEventHandler<T> where T : DomainEvent
{
    Task HandleAsync(T @event);
}
