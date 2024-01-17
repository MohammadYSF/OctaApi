using Command.Core.Application.Common.Exceptions;
using Command.Core.Application.Repositories;
using Command.Core.Domain.InventoryItem;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.InventoryFeatures.UpdateInventoryItem;
public class UpdateInventoryItemHandler : IRequestHandler<UpdateInventoryItemRequest, UpdateInventoryItemResponse>
{
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IInventoryItemCommandRepository _inventoryItemRepository;
    private readonly IEventBus _eventBus;
    public UpdateInventoryItemHandler(ICommandUnitOfWork unitOfWork, IInventoryItemCommandRepository inventoryItemRepository, IEventBus eventBus)
    {
        _unitOfWork = unitOfWork;
        _inventoryItemRepository = inventoryItemRepository;
        _eventBus = eventBus;
    }
    public async Task<UpdateInventoryItemResponse> Handle(UpdateInventoryItemRequest request, CancellationToken cancellationToken)
    {
        var inventoryItemAggregate = await _inventoryItemRepository.GetByIdAsync(request.Id);
        if (inventoryItemAggregate == null)
            throw new AggregateNotFoundException<InventoryItemَAggregate>($"{nameof(InventoryItemَAggregate)} with id {request.Id} not found !");
        inventoryItemAggregate.Update(request.Name, request.BuyPrice, request.SellPrice, request.Count);
        await _inventoryItemRepository.UpdateAsync(inventoryItemAggregate);

        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in inventoryItemAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        var response = new UpdateInventoryItemResponse();
        return response;
    }
}
