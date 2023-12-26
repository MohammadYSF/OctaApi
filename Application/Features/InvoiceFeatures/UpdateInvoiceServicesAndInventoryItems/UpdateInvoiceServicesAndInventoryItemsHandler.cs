﻿using Application.Repositories;
using Domain.SellInvoice;
using MediatR;
using OctaApi.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.UpdateInvoiceServicesAndInventoryItems;
public sealed record class UpdateInvoiceServicesAndInventoryItemsHandler : IRequestHandler<UpdateInvoiceServicesAndInventoryItemsRequest, UpdateInvoiceServicesAndInventoryItemsResponse>
{
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly ISellInvoiceCommandRepository _sellInvoiceRepository;
    public UpdateInvoiceServicesAndInventoryItemsHandler(ICommandUnitOfWork unitOfWork, ISellInvoiceCommandRepository sellInvoiceRepository)
    {
        _unitOfWork = unitOfWork;
        _sellInvoiceRepository = sellInvoiceRepository;
    }
    public async Task<UpdateInvoiceServicesAndInventoryItemsResponse> Handle(UpdateInvoiceServicesAndInventoryItemsRequest request, CancellationToken cancellationToken)
    {
        var datetimeNow = DateTime.Now;
        SellInvoiceAggregate? sellInvoiceAggregate = await _sellInvoiceRepository.GetByIdAsync(request.InvoiceId);

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
            }
        }
        foreach (var item in request.ToRemoveInvoiceServiceIds)
        {
            sellInvoiceAggregate?.RemoveSellInvoiceService(item);
        }
        foreach (var item in request.ToRemoveInvoiceInventoryItemIds)
        {
            sellInvoiceAggregate?.RemoveSellInvoiceInventoryItem(item);
        }

        await _sellInvoiceRepository.UpdateAsync(sellInvoiceAggregate);

       
        await _unitOfWork.SaveAsync(cancellationToken);
        var response = new UpdateInvoiceServicesAndInventoryItemsResponse();
        return response;
    }
}
