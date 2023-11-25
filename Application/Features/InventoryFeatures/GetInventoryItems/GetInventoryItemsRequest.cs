using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OctaApi.Application.Features.Inventory.GetInventoryItems
{
    public record GetInventoryItemsRequest:IRequest<GetInventoryItemsResponse>
    {
        
    }
}
