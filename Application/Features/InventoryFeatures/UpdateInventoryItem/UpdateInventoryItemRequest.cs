using MediatR;
using OctaApi.Application.Features.InventoryFeatures.UpdateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.UpdateInventoryItem
{
    public sealed record UpdateInventoryItemRequest(Guid Id, string Name, long  BuyPrice , long SellPrice,float Count , float CountLowerBound) : IRequest<UpdateInventoryItemResponse>;
}
