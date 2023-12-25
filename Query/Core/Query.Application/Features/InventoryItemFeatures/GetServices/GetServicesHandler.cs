using OctaApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OctaApi.Application.DomainModels;

namespace OctaApi.Application.Features.Inventory.GetServices
{
    public sealed class GetServicesHandler : IRequestHandler<GetServicesRequest, GetServicesResponse>
    {
        private readonly IServiceRepository  _serviceRepository;

        public GetServicesHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<GetServicesResponse> Handle(GetServicesRequest request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetAllAsync();
            var serviceDTOs = services.OrderBy(a=> a.Code).Select((item, index) =>
            {
                return new ServiceDTO(
                    RowNumber: index + 1,
                    Code: item.Code.ToString(),
                    Title: item.Name,
                    Price: item.DefaultPrice,
                    Id:item.Id
                    );
            }).ToList();
            var response = new GetServicesResponse(serviceDTOs);
            return response;
        }
    }
}
