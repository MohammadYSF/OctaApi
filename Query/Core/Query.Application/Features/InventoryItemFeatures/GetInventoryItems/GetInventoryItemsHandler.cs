
using MediatR;
using Query.Application.Repositories;

namespace OctaApi.Application.Features.Inventory.GetInventoryItems;

public sealed class GetInventoryItemsHandler : IRequestHandler<GetInventoryItemsRequest, GetInventoryItemsResponse>
{
    private readonly IInventoryItemQueryRepository _inventoryItemRepository;

    public GetInventoryItemsHandler(IInventoryItemQueryRepository inventoryItemRepository)
    {
        _inventoryItemRepository = inventoryItemRepository;
    }

    public async Task<GetInventoryItemsResponse> Handle(GetInventoryItemsRequest request, CancellationToken cancellationToken)
    {
        var inventories = await _inventoryItemRepository.GetAsync();
        var response = new GetInventoryItemsResponse(inventories);
        return response;
    }
}
