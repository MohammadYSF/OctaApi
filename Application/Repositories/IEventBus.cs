using Application.Common;
using OctaApi.Domain.Common;
namespace Application.Repositories;
public interface IEventBus
{
    void Publish<T>(T @event) where T : DomainEvent;
    void Subscribe<T, TH>() where T : DomainEvent where TH : IEventHandler<T>;
}
