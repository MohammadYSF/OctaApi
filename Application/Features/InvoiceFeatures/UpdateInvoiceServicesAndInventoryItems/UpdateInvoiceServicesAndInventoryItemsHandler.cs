using AutoMapper;
using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.UpdateInvoiceServicesAndInventoryItems
{
    public sealed record class UpdateInvoiceServicesAndInventoryItemsHandler :IRequestHandler<UpdateInvoiceServicesAndInventoryItemsRequest, UpdateInvoiceServicesAndInventoryItemsResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;

        public UpdateInvoiceServicesAndInventoryItemsHandler(IMapper mapper, IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository, IInventoryItemRepository inventoryItemRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
            _inventoryItemRepository = inventoryItemRepository;
        }

        public async Task<UpdateInvoiceServicesAndInventoryItemsResponse> Handle(UpdateInvoiceServicesAndInventoryItemsRequest request, CancellationToken cancellationToken)
        {
            var datetimeNow = DateTime.Now;

            Invoice? invoice = await _invoiceRepository.GetById(request.InvoiceId);
            if (invoice == null && invoice?.Type != Domain.Enums.InvoiceType.Sell)
                throw new Exception("invalid invoice");
            invoice.UseBuyPrice = request.UseBuyPrice;
            invoice.UpdateDate = datetimeNow;
            invoice.Description = request.Description;
            List<InvoiceService> invoiceServices = request.ServiceIdsAndPrices.Select(a => new InvoiceService
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.Id,
                Price = a.Item2,
                ServiceId = a.Item1,
                RegisterDate = datetimeNow

            }).ToList();
            List<InvoiceInventoryItem > invoiceInventoryItems = request.InventoryItemIdsAndCounts.Select(a => new InvoiceInventoryItem
            {
                Id = Guid.NewGuid(),
                InventoryItemId = a.Item1,
                Count = a.Item2,
                InvoiceId = invoice.Id,
                RegisterDate = datetimeNow,
            }).ToList();
            List<InvoiceInventoryItem> toRemoveInvoiceInventoryItems = new();
            foreach (var toRemoveInvoiceInventoryItemId in request.ToRemoveInvoiceInventoryItemIds)
            {
                var invoiceInventoryItemToRemove = await _invoiceRepository.GetInvoiceInventoryItemById(toRemoveInvoiceInventoryItemId);
                if (invoiceInventoryItemToRemove == null) throw new Exception("");
                toRemoveInvoiceInventoryItems.Add(invoiceInventoryItemToRemove);

            }
           var items =  toRemoveInvoiceInventoryItems.Concat(invoiceInventoryItems.Select(a=>
           {
               a.Count *= (-1);
               return a;
           }).ToList()).GroupBy(a => a.InventoryItemId).Select(a => new
            {
                InventoryItemId = a.Key,
                Count = a.Sum(a => a.Count)
            }).ToList();
            foreach (var item in items)
            {
                var inventoryItem = await _inventoryItemRepository.GetByIdAsync(item.InventoryItemId);
                if (inventoryItem == null) throw new Exception("");
                inventoryItem.Count += item.Count;
                //if (inventoryItem.Count < inventoryItem.CountLowerBound) throw new Exception(""); //todo
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
            await _invoiceRepository.DeleteInvoiceInventoryItemsAsync(request.ToRemoveInvoiceInventoryItemIds);
            await _invoiceRepository.DeleteInvoiceServicesAsync(request.ToRemoveInvoiceServiceIds);
            await _invoiceRepository.AddInvoiceInventoryItemsAsync(invoiceInventoryItems.Select(a=>
            {
                a.Count = a.Count < 0 ? (-1) * a.Count : a.Count;
                return a;
            }).ToList());
            await _invoiceRepository.AddInvoiceServicesAsync(invoiceServices);

            _invoiceRepository.Update(invoice);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new UpdateInvoiceServicesAndInventoryItemsResponse(invoice.Id);
            return response;


        }
    }
}
