using MediatR;

namespace Command.Core.Application.Features.InventoryFeatures.UpdateInventoryItem
{
    public sealed record UpdateInventoryItemRequest(Guid Id, string Name, long BuyPrice, long SellPrice, float Count, float CountLowerBound) : IRequest<UpdateInventoryItemResponse>;
}
