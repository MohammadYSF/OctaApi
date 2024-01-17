using Command.Core.Application.Repositories;
using Command.Core.Domain.Service;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.InventoryFeatures.AddService;
public class AddServiceHandler : IRequestHandler<AddServiceRequest, AddServiceResponse>
{
    private readonly IServiceCommandRepository _serviceRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    public AddServiceHandler(IServiceCommandRepository serviceRepository, ICommandUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }
    public async Task<AddServiceResponse> Handle(AddServiceRequest request, CancellationToken cancellationToken)
    {
        int code = await _serviceRepository.GenerateNewCodeAsync();
        Guid serviceId = Guid.NewGuid();
        var serviceAggregate = ServiceAggregate.Create(serviceId, request.Name, code, request.DefaultPrice);
        await _serviceRepository.AddAsync(serviceAggregate);
        await _unitOfWork.SaveAsync(cancellationToken);
        var response = new AddServiceResponse();
        foreach (var item in serviceAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        return response;
    }
}
