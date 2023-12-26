﻿using Application.Repositories;
using Domain.SellInvoice;
using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.DeleteSellInvoice
{
    public sealed class DeleteSellInvoiceHandler : IRequestHandler<DeleteSellInvoiceRequest, DeleteSellInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemCommandRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly ICommandUnitOfWork _unitOfWork;
        private readonly ISellInvoiceCommandRepository _sellInvoiceRepository;
        private readonly IEventBus _eventBus;
        public DeleteSellInvoiceHandler(ICommandUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository, IInventoryItemCommandRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository, ISellInvoiceCommandRepository sellInvoiceRepository, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
            _sellInvoiceRepository = sellInvoiceRepository;
            _eventBus = eventBus;
        }

        public async Task<DeleteSellInvoiceResponse> Handle(DeleteSellInvoiceRequest request, CancellationToken cancellationToken)
        {
            SellInvoiceAggregate? sellInvoiceAggregate = await _sellInvoiceRepository.GetByIdAsync(request.Id);
            sellInvoiceAggregate?.Delete();
            await _sellInvoiceRepository.DeleteAsync(sellInvoiceAggregate!);
            //var invoice = await _invoiceRepository.GetById(request.Id);
            //var invoiceInventoryItems = await _invoiceRepository.GetInvoiceInventoryItemsByInvoiceId(request.Id);
            //var items = invoiceInventoryItems.GroupBy(a => a.InventoryItemId).Select(a => new
            //{
            //    InventoryItemId = a.Key,
            //    Count = a.Sum(b => b.Count)
            //}).ToList();
            //var datetimeNow = DateTime.Now;
            //foreach (var item in items)
            //{
            //    var inventoryItem = await _inventoryItemRepository.GetByIdAsync(item.InventoryItemId);
            //    if (inventoryItem == null) throw new Exception("");
            //    inventoryItem.Count += item.Count;
            //    var inventoryItemHistory = new InventoryItemHistory
            //    {
            //        BuyPrice = inventoryItem.BuyPrice,
            //        Code = inventoryItem.Code,
            //        Count = inventoryItem.Count,
            //        Id = Guid.NewGuid(),
            //        CountLowerBound = inventoryItem.CountLowerBound,
            //        IsActive = inventoryItem.IsActive,
            //        Name = inventoryItem.Name,
            //        SellPrice = inventoryItem.SellPrice,
            //        UpdateDate = datetimeNow,
            //        InventoryItemId = inventoryItem.Id
            //    };
            //    await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistory);
            //    _inventoryItemRepository.Update(inventoryItem);
            //}
            //if (invoice == null)
            //    throw new Exception("invoice not found");
            //_invoiceRepository.Delete(invoice);
            await _unitOfWork.SaveAsync(cancellationToken);
            foreach (var item in sellInvoiceAggregate.GetDomainEvents())
            {
                await _eventBus.Publish(item);   
            }
            return new DeleteSellInvoiceResponse(request.Id);
        }
    }
}
