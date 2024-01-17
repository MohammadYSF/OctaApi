using Command.Core.Application.Common.Exceptions;
using Command.Core.Application.Repositories;
using Command.Core.Domain.Service;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.InventoryFeatures.UpdateService;
public class UpdateServiceHandler : IRequestHandler<UpdateServiceRequest, UpdateServiceResponse>
{
    private readonly IServiceCommandRepository _serviceRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    public UpdateServiceHandler(IServiceCommandRepository serviceRepository, ICommandUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }
    public async Task<UpdateServiceResponse> Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
    {
        var serviceAggregate = await _serviceRepository.GetByIdAsync(request.Id);
        if (serviceAggregate == null)
            throw new AggregateNotFoundException<ServiceAggregate>($"{nameof(ServiceAggregate)} with id {request.Id} not found !");
        serviceAggregate.Update(request.Name, request.DefaultPrice);
        await _serviceRepository.UpdateAsync(serviceAggregate);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in serviceAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        var response = new UpdateServiceResponse();
        return response;
    }
}
