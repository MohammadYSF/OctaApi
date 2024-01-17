using Command.Core.Application.Repositories;
using Command.Core.Domain.InventoryItem;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.InventoryFeatures.AddInventoryItem;
public class AddInventoryItemHandler : IRequestHandler<AddInventoryItemRequest, AddInventoryItemResponse>
{
    private readonly IInventoryItemCommandRepository _inventoryItemRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public AddInventoryItemHandler(IInventoryItemCommandRepository inventoryItemRepository, ICommandUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _inventoryItemRepository = inventoryItemRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<AddInventoryItemResponse> Handle(AddInventoryItemRequest request, CancellationToken cancellationToken)
    {
        int code = await _inventoryItemRepository.GenerateNewCodeAsync();
        var inventoryItemAggregate = InventoryItemَAggregate.Create(Guid.NewGuid(), request.Name, code);

        await _inventoryItemRepository.AddAsync(inventoryItemAggregate);

        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in inventoryItemAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        return new AddInventoryItemResponse();
    }
}
