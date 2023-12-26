using Command.Core.Application.Repositories;
using Command.Core.Domain.BuyInvoice.Entities;
using Command.Core.Domain.InventoryItem;
using Command.Core.Domain.Invoice;
using MediatR;
namespace Command.Core.Application.Features.InvoiceFeatures.CreateBuyInvoice;
public sealed class CreateBuyInvoiceHandler : 
    IRequestHandler<CreateBuyInvoiceRequest, CreateBuyInvoiceResponse>
{
    private readonly IInventoryItemCommandRepository _inventoryItemRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    private readonly IBuyInvoiceCommandRepository _buyInvoiceRepository;
    public CreateBuyInvoiceHandler( ICommandUnitOfWork unitOfWork, IInventoryItemCommandRepository inventoryItemRepository , IEventBus eventBus, IBuyInvoiceCommandRepository buyInvoiceRepository)
    {
        _unitOfWork = unitOfWork;
        _inventoryItemRepository = inventoryItemRepository;
        _eventBus = eventBus;
        _buyInvoiceRepository = buyInvoiceRepository;
    }
    public async Task<CreateBuyInvoiceResponse> Handle(CreateBuyInvoiceRequest request, CancellationToken cancellationToken)
    {
        Guid buyInvoiceId = Guid.NewGuid();
        List<BuyInvoiceInventoryItem> inventoryItems = new();
        foreach (var item in request.Dtos)
        {
            var buyInvoiceInventoryItem = BuyInvoiceInventoryItem.Create(Guid.NewGuid(), buyInvoiceId, item.Id, item.Count);
            inventoryItems.Add(buyInvoiceInventoryItem);
        }
        BuyInvoiceAggregate buyInvoiceAggregate = BuyInvoiceAggregate.Create(buyInvoiceId, request.RegisterDate, request.Code, request.SellerName, inventoryItems);
        List<InventoryItemَAggregate> inventoryItemAggregates = new();
        foreach (var item in request.Dtos)
        {
            var inventoryItemAggregate = await _inventoryItemRepository.GetByIdAsync(item.Id);
            inventoryItemAggregate?.Buy(item.BuyPrice, item.SellPrice, item.Count);
            inventoryItemAggregates.Add(inventoryItemAggregate!);
        }
        await _buyInvoiceRepository.UpdateAsync(buyInvoiceAggregate);
        await _inventoryItemRepository.UpdateAsync(inventoryItemAggregates);
        await _unitOfWork.SaveAsync(cancellationToken);
        var response = new CreateBuyInvoiceResponse();
        return response;
    }
}
