using Application.Common;
using Application.ReadModels;
using Application.Repositories.Query;
using Domain.InventoryItem.Events;
namespace Application.EventHandlers.Query.InventoryItem;
public class InventoryItemEventHandler : IEventHandler<InventoryItemCreatedEvent>
{
    private readonly IQueryUnitOfWork _unitOfWork;
    private readonly IInventoryItemQueryRepository _inventoryItemQueryRepository;

    public InventoryItemEventHandler(IQueryUnitOfWork unitOfWork, IInventoryItemQueryRepository inventoryItemQueryRepository)
    {
        _unitOfWork = unitOfWork;
        _inventoryItemQueryRepository = inventoryItemQueryRepository;
    }

    public async Task HandleAsync(InventoryItemCreatedEvent @event, CancellationToken cancellationToken)
    {
        var inventoryItemRM = new InventoryItemRM
        {
            FromDate = @event.CreateDateTime,
            InventoryItemName = @event.Name,
            Id = Guid.NewGuid(),
            InventoryItemBuyPrice = 0,
            InventoryItemCode = @event.Code.ToString(),
            InventoryItemCount = 0,
            InventoryItemId = @event.InventoryItemId,
            InventoryItemSellPrice = 0,
            ToDate = null
        };
        await _inventoryItemQueryRepository.AddAsync(inventoryItemRM);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}
