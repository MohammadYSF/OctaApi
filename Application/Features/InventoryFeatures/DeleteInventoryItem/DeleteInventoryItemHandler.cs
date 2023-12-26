using Application.Repositories;
using AutoMapper;
using OctaApi.Application.Features.InventoryFeatures.DeleteService;
using OctaApi.Application.Repositories;
namespace OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem;
public class DeleteInventoryItemHandler
{
    private readonly IInventoryItemCommandRepository _inventoryItemRepository;
    private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
    private readonly IMapper _mapper;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    public DeleteInventoryItemHandler(IInventoryItemCommandRepository serviceRepository, IInventoryItemHistoryRepository serviceHistoryRepository, IMapper mapper, IEventBus eventBus)
    {
        _inventoryItemRepository = serviceRepository;
        _inventoryItemHistoryRepository = serviceHistoryRepository;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<DeleteInventoryItemResponse> Handle(DeleteServiceRequest request, CancellationToken cancellationToken)
    {
        var inventoryItemAggregaet = await _inventoryItemRepository.GetByCodeAsync(request.Code);
        if (inventoryItemAggregaet == null)
            throw new Exception(""); //todo
        //inventoryItem.IsActive = false;
        //var inventoryItemHistory = _mapper.Map<InventoryItemHistory>(inventoryItem);
        //_inventoryItemRepository.Update(inventoryItem);
        //await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistory);
        inventoryItemAggregaet.Delete();
        await _inventoryItemRepository.UpdateAsync(inventoryItemAggregaet);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in inventoryItemAggregaet.GetDomainEvents())
        {
            await _eventBus.PublishAsync(item);
        }
        //var response = new DeleteInventoryItemResponse(inventoryItem.Id);
        var response = new DeleteInventoryItemResponse();
        return response;
    }
}
