using MediatR;
using OctaApi.Application.Features.InventoryFeatures.AddService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.UpdateService
{
    public sealed record UpdateServiceRequest(Guid Id,string Name, long DefaultPrice) : IRequest<UpdateServiceResponse>;
}
