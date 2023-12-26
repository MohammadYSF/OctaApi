using Application.Repositories;
using AutoMapper;
using MediatR;
using OctaApi.Application.Repositories;
namespace OctaApi.Application.Features.InventoryFeatures.UpdateService;
public class UpdateServiceHandler : IRequestHandler<UpdateServiceRequest, UpdateServiceResponse>
{
    private readonly IServiceCommandRepository _serviceRepository;
    private readonly IServiceHistoryRepository _serviceHistoryRepository;
    private readonly IMapper _mapper;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    public UpdateServiceHandler(IServiceHistoryRepository serviceHistoryRepository, IServiceCommandRepository serviceRepository, IMapper mapper, ICommandUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _serviceHistoryRepository = serviceHistoryRepository;
        _serviceRepository = serviceRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }
    public async Task<UpdateServiceResponse> Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
    {
        //Service? service = await _serviceRepository.GetByIdAsync(request.Id);
        var serviceAggregate = await _serviceRepository.GetByIdAsync(request.Id);
        if (serviceAggregate == null)
            throw new Exception("");
        //service.DefaultPrice = request.DefaultPrice;
        //service.Name = request.Name;
        //_serviceRepository.Update(service);
        //var serviceHistory = _mapper.Map<ServiceHistory>(service);
        //await _serviceHistoryRepository.AddAsync(serviceHistory);
        serviceAggregate.Update(request.Name, request.DefaultPrice);
        await _serviceRepository.UpdateAsync(serviceAggregate);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in serviceAggregate.GetDomainEvents())
        {
            await _eventBus.Publish(item);
        }
        //var response = new UpdateServiceResponse(service.Id);
        var response = new UpdateServiceResponse();
        return response;
    }
}
