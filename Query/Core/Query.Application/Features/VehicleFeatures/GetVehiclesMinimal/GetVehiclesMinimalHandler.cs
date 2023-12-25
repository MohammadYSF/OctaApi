using MediatR;
using OctaApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.VehicleFeatures.GetVehiclesMinimal
{
    public sealed class GetVehiclesMinimalHandler : IRequestHandler<GetVehiclesMinimalRequest, GetVehiclesMinimalResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehiclesMinimalHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<GetVehiclesMinimalResponse> Handle(GetVehiclesMinimalRequest request, CancellationToken cancellationToken)
        {
            var data = (await _vehicleRepository.GetAllAsync()).Select(a => new GetVehiclesMinimal_DTO(a.Id, a.Code, a.Name)).ToList();
            var response = new GetVehiclesMinimalResponse(data);
            return response;
        }
    }
}
