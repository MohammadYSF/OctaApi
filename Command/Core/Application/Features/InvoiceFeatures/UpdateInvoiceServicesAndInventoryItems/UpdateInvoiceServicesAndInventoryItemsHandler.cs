using Command.Core.Application.Common.Exceptions;
using Command.Core.Application.Repositories;
using Command.Core.Domain.InventoryItem;
using Command.Core.Domain.SellInvoice;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.InvoiceFeatures.UpdateInvoiceServicesAndInventoryItems;
public sealed record class UpdateInvoiceServicesAndInventoryItemsHandler : IRequestHandler<UpdateInvoiceServicesAndInventoryItemsRequest, UpdateInvoiceServicesAndInventoryItemsResponse>
{
    private readonly IInventoryItemCommandRepository _inventoryItemCommandRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly ISellInvoiceCommandRepository _sellInvoiceRepository;
    private readonly IEventBus _eventBus;
    public UpdateInvoiceServicesAndInventoryItemsHandler(ICommandUnitOfWork unitOfWork, ISellInvoiceCommandRepository sellInvoiceRepository, IEventBus eventBus, IInventoryItemCommandRepository inventoryItemCommandRepository)
    {
        _unitOfWork = unitOfWork;
        _sellInvoiceRepository = sellInvoiceRepository;
        _eventBus = eventBus;
        _inventoryItemCommandRepository = inventoryItemCommandRepository;
    }
    public async Task<UpdateInvoiceServicesAndInventoryItemsResponse> Handle(UpdateInvoiceServicesAndInventoryItemsRequest request, CancellationToken cancellationToken)
    {
        var datetimeNow = DateTime.UtcNow;
        SellInvoiceAggregate? sellInvoiceAggregate = await _sellInvoiceRepository.GetByIdAsync(request.InvoiceId);
        if (sellInvoiceAggregate == null)
            throw new AggregateNotFoundException<SellInvoiceAggregate>($"{nameof(SellInvoiceAggregate)} with id {request.InvoiceId} not found !");
        sellInvoiceAggregate?.UpdateDescription(request.Description);
        sellInvoiceAggregate?.SetUseBuyPrice(request.UseBuyPrice);
        foreach (var item in request.ServiceIdsAndPrices)
        {
            if (sellInvoiceAggregate?.Services.Select(a => a.ServiceId).Any(a => a == item.Item1) == false)
            {
                sellInvoiceAggregate.AddSellInvoiceService(Guid.NewGuid(), item.Item1, item.Item2);

            }
        }
        foreach (var item in request.InventoryItemIdsAndCounts)
        {
            if (sellInvoiceAggregate?.InventoryItems.Select(a => a.InventoryItemId).Any(a => a == item.Item1) == false)
            {
                sellInvoiceAggregate.AddSellInvoiceInventoryItem(Guid.NewGuid(), item.Item1, item.Item2);
                var inventoryItemAggregate = await _inventoryItemCommandRepository.GetByIdAsync(item.Item1);
                if (inventoryItemAggregate == null) throw new AggregateNotFoundException<InventoryItemَAggregate>($"{nameof(InventoryItemَAggregate)} with id {item.Item1} not found !");
                inventoryItemAggregate.Count = new Domain.InventoryItem.ValueObjects.InventoryItemCount(inventoryItemAggregate.Count.Value - item.Item2);
                await _inventoryItemCommandRepository.UpdateAsync(inventoryItemAggregate);
            }
        }
        foreach (var item in request.ToRemoveInvoiceServiceIds)
        {
            sellInvoiceAggregate?.RemoveSellInvoiceService(item);
        }
        foreach (var item in request.ToRemoveInvoiceInventoryItemIds)
        {
            sellInvoiceAggregate?.RemoveSellInvoiceInventoryItem(item);
            var x = sellInvoiceAggregate.InventoryItems.FirstOrDefault(a => a.Id == item);
            if (x == null) throw new Exception("");
            Guid inventoryItemId = x.InventoryItemId;
            var inventoryItemAggregate = await _inventoryItemCommandRepository.GetByIdAsync(inventoryItemId);
            if (inventoryItemAggregate == null) throw new AggregateNotFoundException<InventoryItemَAggregate>($"{nameof(InventoryItemَAggregate)} with id {inventoryItemId} not found !");
            inventoryItemAggregate.Count = new Domain.InventoryItem.ValueObjects.InventoryItemCount(inventoryItemAggregate.Count.Value - x.Count);
            await _inventoryItemCommandRepository.UpdateAsync(inventoryItemAggregate);


        }

        await _sellInvoiceRepository.UpdateAsync(sellInvoiceAggregate);


        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in sellInvoiceAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        var response = new UpdateInvoiceServicesAndInventoryItemsResponse();
        return response;
    }
}
