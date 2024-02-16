
using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;

namespace OctaApi.Application.Features.Inventory.GetInventoryItems;

public sealed class GetInventoryItemsHandler : IRequestHandler<GetInventoryItemsRequest, GetInventoryItemsResponse>
{
    private readonly IInventoryItemQueryRepository _inventoryItemRepository;
    private readonly IDistributedCacheService<InventoryItemRM> _inventoryItemCacheService;


    public GetInventoryItemsHandler(IInventoryItemQueryRepository inventoryItemRepository, IDistributedCacheService<InventoryItemRM> inventoryItemCacheService)
    {
        _inventoryItemRepository = inventoryItemRepository;
        _inventoryItemCacheService = inventoryItemCacheService;
    }

    public async Task<GetInventoryItemsResponse> Handle(GetInventoryItemsRequest request, CancellationToken cancellationToken)
    {
        await _inventoryItemRepository.CheckCacheAsync();
        var inventories = _inventoryItemCacheService.GetAll().Where(a => (!a.ToDate.HasValue) && (!a.IsDeleted)).ToList();
        //var inventories = await _inventoryItemRepository.GetAsync();
        var response = new GetInventoryItemsResponse(inventories);
        return response;
    }
}
