using AutoMapper;
using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.UpdateService
{
    public class UpdateServiceHandler : IRequestHandler<UpdateServiceRequest, UpdateServiceResponse>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceHistoryRepository  _serviceHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceHandler(IServiceHistoryRepository serviceHistoryRepository, IServiceRepository serviceRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _serviceHistoryRepository = serviceHistoryRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateServiceResponse> Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
        {
            Service? service = await _serviceRepository.GetByIdAsync(request.Id);
            if (service == null)
                throw new Exception("");
            service.DefaultPrice = request.DefaultPrice;
            service.Name = request.Name;
            _serviceRepository.Update(service);
            var serviceHistory = _mapper.Map<ServiceHistory>(service);
            await _serviceHistoryRepository.AddAsync(serviceHistory);
            await _unitOfWork.SaveAsync(cancellationToken);

            var response = new UpdateServiceResponse(service.Id);
            return response;
        }
    }
}
