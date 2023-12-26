using Application.Repositories;
using AutoMapper;
using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.InventoryItem;

namespace OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
public class AddInventoryItemHandler : IRequestHandler<AddInventoryItemRequest, AddInventoryItemResponse>
{
    private readonly IInventoryItemCommandRepository _inventoryItemRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public AddInventoryItemHandler( IInventoryItemCommandRepository inventoryItemRepository,ICommandUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _inventoryItemRepository = inventoryItemRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<AddInventoryItemResponse> Handle(AddInventoryItemRequest request, CancellationToken cancellationToken)
    {
        int code = await _inventoryItemRepository.GenerateNewCodeAsync();
        //var inventoryItemToAdd = _mapper.Map<InventoryItem>(request);
        //int code = await _inventoryItemRepository.GetNewCode();
        var inventoryItemAggregate = InventoryItemَAggregate.Create(Guid.NewGuid(), request.Name, code);

        //inventoryItemToAdd.Code = code;
        await _inventoryItemRepository.AddAsync(inventoryItemAggregate);
        //await _inventoryItemRepository.AddAsync(inventoryItemToAdd);
        //var inventoryItemHistoryToAdd = _mapper.Map<InventoryItemHistory>(request);
        //inventoryItemHistoryToAdd.InventoryItemId = inventoryItemToAdd.Id;
        //inventoryItemHistoryToAdd.Code = code;
        //await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistoryToAdd);
        //var response = new AddInventoryItemResponse(inventoryItemToAdd.Id);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in inventoryItemAggregate.GetDomainEvents())
        {
             _eventBus.Publish(item);
        }
        //return response;
        return new AddInventoryItemResponse();
    }
}
