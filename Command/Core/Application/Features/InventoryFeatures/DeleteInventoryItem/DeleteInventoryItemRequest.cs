using MediatR;

namespace Command.Core.Application.Features.InventoryFeatures.DeleteInventoryItem;

public sealed record DeleteInventoryItemRequest(int Code) : IRequest<DeleteInventoryItemResponse>;
