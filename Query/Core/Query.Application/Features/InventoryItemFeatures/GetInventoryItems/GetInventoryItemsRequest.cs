using MediatR;

namespace OctaApi.Application.Features.Inventory.GetInventoryItems
{
    public record GetInventoryItemsRequest : IRequest<GetInventoryItemsResponse>
    {

    }
}
