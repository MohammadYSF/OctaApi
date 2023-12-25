using MediatR;
using OctaApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed class GetSellInvoiceInventoryItemsHandler : IRequestHandler<GetSellInvoiceInventoryItemsRequest, GetSellInvoiceInventoryItemsResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;

        public GetSellInvoiceInventoryItemsHandler(IInvoiceRepository invoiceRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
        }

        public async Task<GetSellInvoiceInventoryItemsResponse> Handle(GetSellInvoiceInventoryItemsRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetById(request.InvoiceId);
            if (invoice == null) throw new Exception("invoice not found");
            var invoiceInventoryItems = await _invoiceRepository.GetSellInvoiceInventoryItemsAsync(request.InvoiceId);
            List<GetSellInvoiceInventoryItems_DTO> answer = new();
            int i = 0;
            foreach (var a in invoiceInventoryItems)
            {
                var inventoryItemHistory = await _inventoryItemHistoryRepository.GetLatestByInventoryItemIdAndDateAsync(a.InventoryItemId, a.RegisterDate);
                answer.Add(new GetSellInvoiceInventoryItems_DTO(RowNumber: i + 1,
                InventoryItemCode: inventoryItemHistory.Code.ToString(),
                InvoiceInventoryItemId: a.Id,
                InventoryItemId: a.InventoryItemId,
                InventoryItemName: a.InventoryItem.Name,
                InventoryItemCount: a.Count,
                UnitBuyPrice: inventoryItemHistory.BuyPrice.Value,
                UnitSellPrice: inventoryItemHistory.SellPrice.Value,
                TotalPrice: a.Count * inventoryItemHistory.SellPrice.Value));
            }

            var response = new GetSellInvoiceInventoryItemsResponse(Data: answer);
            return response;

        }
    }
}
