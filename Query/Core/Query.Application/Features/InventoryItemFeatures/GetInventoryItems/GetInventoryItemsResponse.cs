using Application.ReadModels;
namespace OctaApi.Application.Features.Inventory.GetInventoryItems
{
    public sealed record GetInventoryItemsResponse(List<InventoryItemRM> InventoryItemDTOs);
}
