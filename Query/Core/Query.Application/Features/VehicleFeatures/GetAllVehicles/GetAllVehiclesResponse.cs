using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.VehicleFeatures.GetAllVehicles
{
    public sealed record GetAllVehiclesResponse
    {
        public int Count { get; set; }
        public List<GetAllVehiclesResponse_DTO> Data { get; set; }
    }
}
