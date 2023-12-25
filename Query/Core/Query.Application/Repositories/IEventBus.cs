using OctaApi.Domain.Common;
namespace Application.Repositories;
public interface IEventBus
{
    Task PublishAsync(DomainEvent @event);
}
