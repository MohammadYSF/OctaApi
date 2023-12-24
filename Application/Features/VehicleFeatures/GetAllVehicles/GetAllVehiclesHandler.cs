using Application.Repositories.Command;
using MediatR;
using OctaApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.VehicleFeatures.GetAllVehicles
{
    public sealed record GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesRequest , GetAllVehiclesResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerCommandRepository _customerRepository;

        public GetAllVehiclesHandler(IVehicleRepository vehicleRepository, ICustomerCommandRepository customerRepository)
        {
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
        }

        public async Task<GetAllVehiclesResponse> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            var dtoList = new List<GetAllVehiclesResponse_DTO>();
            int i = 1;
            foreach (var vehicle in vehicles)
            {
                var customer = await _customerRepository.GetByIdAsync(vehicle.CustomerId);
                dtoList.Add(new GetAllVehiclesResponse_DTO
                {
                    CustomerId = customer.Id,
                    CustomerCode = customer.Code.ToString(),
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    RowNumber = i,
                    VehicleCode = vehicle.Code.ToString(),
                    VehicleId = vehicle.Id,
                    VehicleName = vehicle.Name
                });
                i++;
            }
            var response = new GetAllVehiclesResponse()
            {
                Count = dtoList.Count,
                Data = dtoList
            };
            return response;
        }
    }
}
