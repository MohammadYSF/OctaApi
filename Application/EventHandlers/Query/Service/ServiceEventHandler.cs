using Application.Common;
using Application.ReadModels;
using Application.Repositories.Query;
using Domain.Service.Events;

namespace Application.EventHandlers.Query.Service;

public class ServiceEventHandler : IEventHandler<ServiceCreatedEvent>
{
    private readonly IServiceQueryRepository _serviceQueryRepository;
    private readonly IQueryUnitOfWork _queryUnitOfWork;

    public ServiceEventHandler(IServiceQueryRepository serviceQueryRepository, IQueryUnitOfWork queryUnitOfWork)
    {
        _serviceQueryRepository = serviceQueryRepository;
        _queryUnitOfWork = queryUnitOfWork;
    }

    public async Task HandleAsync(ServiceCreatedEvent @event, CancellationToken cancellationToken)
    {
        var serviceRM = new ServicecRM
        {
            FromDate = @event.CreateDateTime,
            Id = Guid.NewGuid(),
            ServiceCode = @event.Code.ToString(),
            ServiceDefaultPrice = @event.DefaultPrice,
            ServiceId = @event.ServiceId,
            ServiceName = @event.Name,
            ToDate = null,
        };
        await _serviceQueryRepository.AddAsync(serviceRM);
        await _queryUnitOfWork.SaveAsync(cancellationToken);
    }
}
