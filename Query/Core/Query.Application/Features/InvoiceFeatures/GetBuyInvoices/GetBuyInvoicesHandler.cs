using MediatR;
using OctaApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetBuyInvoices
{
    public sealed class GetBuyInvoicesHandler : IRequestHandler<GetBuyInvoicesRequest, GetBuyInvoicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;

        public GetBuyInvoicesHandler(IInvoiceRepository invoiceRepository, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository, IServiceHistoryRepository serviceHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
            _serviceHistoryRepository = serviceHistoryRepository;
        }
        private async Task<float> CalculateInvoiceTotalCost(Guid invoiceId)
        {
            float total = 0;
            var invoice = await _invoiceRepository.GetById(invoiceId);
            bool invoiceUseBuyPrice = invoice.UseBuyPrice.HasValue ? invoice.UseBuyPrice.Value : false;
            var invoiceInventoryItems = await _invoiceRepository.GetInvoiceInventoryItemsByInvoiceId(invoiceId);
            var invoiceServiceItems = await _invoiceRepository.GetInvoiceServicesByInvoiceIdAsync(invoiceId);
            foreach (var item in invoiceInventoryItems)
            {
                var inventoryItemHistory = await _inventoryItemHistoryRepository.GetLatestByInventoryItemIdAndDateAsync(item.InventoryItemId, item.RegisterDate);
                total += item.Count * inventoryItemHistory.BuyPrice.Value;
            }

            foreach (var item in invoiceServiceItems)
            {
                var serviceHistory = await _serviceHistoryRepository.GetLatestServiceHistoryByServiceIdAndDate(item.ServiceId, item.RegisterDate);
                total += (1 * item.Price);
            }
            return total;
        }
        public async Task<GetBuyInvoicesResponse> Handle(GetBuyInvoicesRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            var inventoryItemHistories = await _inventoryItemHistoryRepository.GetAllAsync();
            var invoicePayments = await _invoiceRepository.GetAllInvoicePaymentsAsync();
            var invoiceInventoryItems = await _invoiceRepository.GetAllInvoiceInventoryItemsAsync();
            var data = invoices.Where(a => a.Type == Domain.Enums.InvoiceType.Buy).ToList();
            var answer = new List<GetBuyInvoices_InvoiceDTO>();
            int i = 0;
            foreach (var a in data)
            {
                long paidAmount = invoicePayments.Where(b => b.InvoiceId == a.Id).Select(b => b.PaidAmount).Sum();
                float total = await this.CalculateInvoiceTotalCost(a.Id);
                answer.Add(new GetBuyInvoices_InvoiceDTO
                 (
                     SellerName: a.SerllerName,
                     InvoiceCode: a.Code.ToString(),
                     InvoiceDate: a.RegisterDate,
                     InvoiceDateString: a.RegisterDate.ToString(),
                     InvoiceId: a.Id,
                     RowNumber: i + 1,
                     InvoiceTotalPrice: total,
                     InvoicePaidAmount: paidAmount

                 ));
            }
            var response = new GetBuyInvoicesResponse(Data: answer);
            return response;
            //var data = await _invoiceRepository.GetBuyInvoicesAsync();
            //return response;
        }
    }
}
