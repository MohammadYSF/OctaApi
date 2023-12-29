using Query.Application.Common.Exceptions;
using Query.Application.Core;
using Query.Application.Events.Services;
using Query.Application.ReadModels;
using Query.Application.Repositories;

namespace Query.Application.EventHandlers.Service;

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

    public async Task HandleAsync(ServiceCreatedEvent @event)
    {
        var serviceRM = new ServiceRM
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
        await _queryUnitOfWork.SaveAsync(default);
    }

    public async Task HandleAsync(ServiceUpdatedEvent @event)
    {
        try
        {
            ServiceRM? prevRM = (await _serviceQueryRepository.GetByServiceIdAsync(@event.ServiceId)).FirstOrDefault(a => !a.ToDate.HasValue);
            if (prevRM == null) throw new ReadModelNotFoundException<ServiceRM>();

            prevRM.ToDate = @event.UpdateDate;
            var newServiceRM = new ServiceRM
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
            await _queryUnitOfWork.SaveAsync(default);
        }
        catch (Exception e)
        {
            //todo:handle the error
        }
    }
}
