using OctaApi.Application.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.Inventory.GetServices
{
    public sealed record GetServicesResponse(List<ServiceDTO> ServiceDTOs);
}