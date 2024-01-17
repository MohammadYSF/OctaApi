using Command.Core.Application.Common.Exceptions;
using Command.Core.Application.Repositories;
using Command.Core.Domain.Service;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.InventoryFeatures.DeleteService;
public sealed class DeleteServiceHandler : IRequestHandler<DeleteServiceRequest, DeleteServiceResponse>
{
    private readonly IServiceCommandRepository _serviceRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;


    public DeleteServiceHandler(IServiceCommandRepository serviceRepository, IEventBus eventBus)
    {
        _serviceRepository = serviceRepository;
        _eventBus = eventBus;
    }

    public async Task<DeleteServiceResponse> Handle(DeleteServiceRequest request, CancellationToken cancellationToken)
    {
        var serviceAggregate = await _serviceRepository.GetByCodeAsync(request.Code);
        if (serviceAggregate == null)
            throw new AggregateNotFoundException<ServiceAggregate>($"{nameof(ServiceAggregate)} with code {request.Code} not found !");
        serviceAggregate.Delete();
        await _serviceRepository.UpdateAsync(serviceAggregate);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in serviceAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        var response = new DeleteServiceResponse();
        return response;
    }

}
