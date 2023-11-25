﻿using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.DeleteSellInvoiuce
{
    public sealed class DeleteSellInvoiceHandler : IRequestHandler<DeleteSellInvoiceRequest, DeleteSellInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSellInvoiceHandler(IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
        }

        public async Task<DeleteSellInvoiceResponse> Handle(DeleteSellInvoiceRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetById(request.Id);
            var invoiceInventoryItems = await _invoiceRepository.GetInvoiceInventoryItemsByInvoiceId(request.Id);
            var items = invoiceInventoryItems.GroupBy(a => a.InventoryItemId).Select(a => new
            {
                InventoryItemId = a.Key,
                Count = a.Sum(b => b.Count)
            }).ToList();
            var datetimeNow = DateTime.Now;
            foreach (var item in items)
            {
                var inventoryItem = await _inventoryItemRepository.GetByIdAsync(item.InventoryItemId);
                if (inventoryItem == null) throw new Exception("");
                inventoryItem.Count += item.Count;                
                var inventoryItemHistory = new InventoryItemHistory
                {
                    BuyPrice = inventoryItem.BuyPrice,
                    Code = inventoryItem.Code,
                    Count = inventoryItem.Count,
                    Id = Guid.NewGuid(),
                    CountLowerBound = inventoryItem.CountLowerBound,
                    IsActive = inventoryItem.IsActive,
                    Name = inventoryItem.Name,
                    SellPrice = inventoryItem.SellPrice,
                    UpdateDate = datetimeNow,
                    InventoryItemId = inventoryItem.Id
                };
                await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistory);
                 _inventoryItemRepository.Update(inventoryItem);
            }
            if (invoice == null)
                throw new Exception("invoice not found");
            _invoiceRepository.Delete(invoice);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new DeleteSellInvoiceResponse(request.Id);
        }
    }
}