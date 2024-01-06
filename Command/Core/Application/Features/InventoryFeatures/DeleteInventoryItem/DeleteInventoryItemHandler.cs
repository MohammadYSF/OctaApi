using Command.Core.Application.Common.Exceptions;
using Command.Core.Application.Repositories;
using Command.Core.Domain.InventoryItem;
namespace Command.Core.Application.Features.InventoryFeatures.DeleteInventoryItem;
public class DeleteInventoryItemHandler
{
    private readonly IInventoryItemCommandRepository _inventoryItemRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    public DeleteInventoryItemHandler(IInventoryItemCommandRepository serviceRepository, IEventBus eventBus)
    {
        _inventoryItemRepository = serviceRepository;
        _eventBus = eventBus;
    }

    public async Task<DeleteInventoryItemResponse> Handle(DeleteInventoryItemRequest request, CancellationToken cancellationToken)
    {
        var inventoryItemAggregaet = await _inventoryItemRepository.GetByCodeAsync(request.Code);
        if (inventoryItemAggregaet == null)
            throw new AggregateNotFoundException<InventoryItemَAggregate>($"{nameof(InventoryItemَAggregate)} with code {request.Code} not found !");
        inventoryItemAggregaet.Delete();
        await _inventoryItemRepository.UpdateAsync(inventoryItemAggregaet);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in inventoryItemAggregaet.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        var response = new DeleteInventoryItemResponse();
        return response;
    }
}
