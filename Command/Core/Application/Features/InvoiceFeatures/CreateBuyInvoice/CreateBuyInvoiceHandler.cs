﻿using Command.Core.Application.Repositories;
using Command.Core.Domain.BuyInvoice.Entities;
using Command.Core.Domain.InventoryItem;
using Command.Core.Domain.Invoice;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.InvoiceFeatures.CreateBuyInvoice;
public sealed class CreateBuyInvoiceHandler :
    IRequestHandler<CreateBuyInvoiceRequest, CreateBuyInvoiceResponse>
{
    private readonly IInventoryItemCommandRepository _inventoryItemRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    private readonly IBuyInvoiceCommandRepository _buyInvoiceRepository;
    public CreateBuyInvoiceHandler(ICommandUnitOfWork unitOfWork, IInventoryItemCommandRepository inventoryItemRepository, IEventBus eventBus, IBuyInvoiceCommandRepository buyInvoiceRepository)
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
            var buyInvoiceInventoryItem = BuyInvoiceInventoryItem.Create(Guid.NewGuid(), buyInvoiceId, item.Id, item.Count, item.BuyPrice, item.SellPrice);
            inventoryItems.Add(buyInvoiceInventoryItem);
        }
        BuyInvoiceAggregate buyInvoiceAggregate = BuyInvoiceAggregate.Create(buyInvoiceId, request.RegisterDate, request.Code, request.SellerName, inventoryItems);
        List<InventoryItemَAggregate> inventoryItemAggregates = new();
        foreach (var item in request.Dtos)
        {
            var inventoryItemAggregate = await _inventoryItemRepository.GetByIdAsync(item.Id);
            inventoryItemAggregate?.Buy(item.BuyPrice, item.SellPrice, item.Count);
            inventoryItemAggregates.Add(inventoryItemAggregate!);
            foreach (var item2 in inventoryItemAggregate.GetDomainEvents())
            {
                _eventBus.Publish(item2);
            }
        }
        await _buyInvoiceRepository.CreateAsync(buyInvoiceAggregate);
        await _inventoryItemRepository.UpdateAsync(inventoryItemAggregates);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in buyInvoiceAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        var response = new CreateBuyInvoiceResponse();
        return response;
    }
}
