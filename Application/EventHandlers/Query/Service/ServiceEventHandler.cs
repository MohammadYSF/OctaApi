using Application.Common;
using Application.ReadModels;
using Application.Repositories.Query;
using Domain.Service.Events;

namespace Application.EventHandlers.Query.Service;

public class ServiceEventHandler :
    IEventHandler<ServiceCreatedEvent>
    , IEventHandler<ServiceUpdatedEvent>

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

    public async Task HandleAsync(ServiceUpdatedEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            var prevRM = (await _serviceQueryRepository.GetByServiceIdAsync(@event.ServiceId)).FirstOrDefault(a => !a.ToDate.HasValue);
            prevRM.ToDate = @event.UpdateDate;
            var newServiceRM = new ServicecRM
            {
                FromDate = @event.UpdateDate,
                ToDate = null,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                ServiceCode = @event.Code.ToString(),
                ServiceDefaultPrice = @event.NewDefaultPrice,
                ServiceId = @event.ServiceId,
                ServiceName = @event.NewName,
            };
            await _serviceQueryRepository.UpdateAsync(prevRM);
            await _serviceQueryRepository.AddAsync(newServiceRM);
            await _queryUnitOfWork.SaveAsync(cancellationToken);
        }
        catch (Exception e)
        {
            //todo:handle the error
        }
    }
}
