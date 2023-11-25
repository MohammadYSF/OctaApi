using AutoMapper;
using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.DeleteService
{
    public sealed class DeleteServiceHandler : IRequestHandler<DeleteServiceRequest, DeleteServiceResponse>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceHandler(IServiceRepository serviceRepository, IServiceHistoryRepository serviceHistoryRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _serviceHistoryRepository = serviceHistoryRepository;
            _mapper = mapper;
        }

        public async Task<DeleteServiceResponse> Handle(DeleteServiceRequest request, CancellationToken cancellationToken)
        {
            var service = await  _serviceRepository.GetByCode(request.Code);
            if (service == null)
                throw new Exception(""); //todo
            service.IsActive = false;
            var serviceHistory = _mapper.Map<ServiceHistory>(service);            
            _serviceRepository.Update(service);
            await _serviceHistoryRepository.AddAsync(serviceHistory);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new DeleteServiceResponse(service.Id);
            return response;
        }

    }
}
