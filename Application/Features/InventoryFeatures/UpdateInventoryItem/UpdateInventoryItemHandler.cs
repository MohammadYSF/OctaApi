using Application.Repositories;
using MediatR;
using OctaApi.Application.Repositories;
namespace OctaApi.Application.Features.InventoryFeatures.UpdateInventoryItem;
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
        //InventoryItem? inventoryItem = await _inventoryItemRepository.GetByIdAsync(request.Id);
        if (inventoryItemAggregate == null)
            throw new Exception("");
        //var inventoryItemNew = _mapper.Map<InventoryItem>(request);
        //inventoryItem.Code = inventoryItem.Code;
        //inventoryItem.SellPrice = request.SellPrice;
        //inventoryItem.BuyPrice = request.BuyPrice;
        //inventoryItem.Count = request.Count;
        //inventoryItem.CountLowerBound = request.CountLowerBound;
        //inventoryItem.Name = request.Name;
        inventoryItemAggregate.Update(request.Name, request.BuyPrice, request.SellPrice, request.Count);
        await _inventoryItemRepository.UpdateAsync(inventoryItemAggregate);
        //_inventoryItemRepository.Update(inventoryItem);
        //var inventoryItemHistory = _mapper.Map<InventoryItemHistory>(inventoryItem);
        //await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistory);            
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in inventoryItemAggregate.GetDomainEvents())
        {
             _eventBus.Publish(item);
        }
        //var response = new UpdateInventoryItemResponse(inventoryItem.Id);
        var response = new UpdateInventoryItemResponse();
        return response;
    }
}
