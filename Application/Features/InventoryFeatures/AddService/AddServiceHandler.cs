using Application.Repositories;
using AutoMapper;
using Domain.Service;
using MediatR;
using OctaApi.Application.Repositories;

namespace OctaApi.Application.Features.InventoryFeatures.AddService
{
    public class AddServiceHandler : IRequestHandler<AddServiceRequest, AddServiceResponse>
    {
        private readonly IServiceCommandRepository _serviceRepository;
        private readonly ICommandUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;

        public AddServiceHandler(IServiceCommandRepository serviceRepository,  ICommandUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<AddServiceResponse> Handle(AddServiceRequest request, CancellationToken cancellationToken)
        {
            //var serviceToAdd = _mapper.Map<Service>(request);
            //int code = await _serviceRepository.GetNewCodeAsync();
            int code = await _serviceRepository.GenerateNewCodeAsync();
            Guid serviceId = Guid.NewGuid();
            //serviceToAdd.Code = code;
            var serviceAggregate = ServiceAggregate.Create(serviceId, request.Name, code, request.DefaultPrice);
            //await _serviceRepository.AddAsync(serviceToAdd);
            //var serviceHistoryToAdd = _mapper.Map<ServiceHistory>(request);
            //serviceHistoryToAdd.ServiceId = serviceToAdd.Id;
            //await _serviceHistoryRepository.AddAsync(serviceHistoryToAdd);
            await _serviceRepository.AddAsync(serviceAggregate);
            await _unitOfWork.SaveAsync(cancellationToken);
            //var response = new AddServiceResponse(serviceToAdd.Id);
            var response = new AddServiceResponse();
            foreach (var item in serviceAggregate.GetDomainEvents())
            {
                 _eventBus.Publish(item);
            }
            return response;
        }
    }
}
