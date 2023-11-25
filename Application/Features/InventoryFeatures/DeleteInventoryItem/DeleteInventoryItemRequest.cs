using MediatR;
using OctaApi.Application.Features.InventoryFeatures.DeleteService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem
{
    public sealed record DeleteInventoryItemRequest(int Code) : IRequest<DeleteInventoryItemResponse>;

}
