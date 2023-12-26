using MediatR;

namespace Command.Core.Application.Features.InventoryFeatures.AddInventoryItem
{
    public sealed record AddInventoryItemRequest(string Name) :IRequest<AddInventoryItemResponse>;    
}
