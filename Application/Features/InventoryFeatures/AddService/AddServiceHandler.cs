using Application.Repositories;
using AutoMapper;
using Domain.Service;
using MediatR;
using OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.AddService
{
    public class AddServiceHandler : IRequestHandler<AddServiceRequest, AddServiceResponse>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;

        public AddServiceHandler(IServiceRepository serviceRepository, IServiceHistoryRepository serviceHistoryRepository, IMapper mapper, IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _serviceRepository = serviceRepository;
            _serviceHistoryRepository = serviceHistoryRepository;
            _mapper = mapper;
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
                await _eventBus.PublishAsync(item);
            }
            return response;
        }
    }
}
