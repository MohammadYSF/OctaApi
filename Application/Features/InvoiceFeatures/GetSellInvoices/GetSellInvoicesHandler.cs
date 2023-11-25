using MediatR;
using OctaApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices
{
    public sealed class GetSellInvoicesHandler : IRequestHandler<GetSellInvoicesRequest, GetSellInvoicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetSellInvoicesHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository, IServiceHistoryRepository serviceHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
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
                total += invoiceUseBuyPrice ? (item.Count * inventoryItemHistory.BuyPrice.Value) : (item.Count * inventoryItemHistory.SellPrice.Value);
            }

            foreach (var item in invoiceServiceItems)
            {
                var serviceHistory = await _serviceHistoryRepository.GetLatestServiceHistoryByServiceIdAndDate(item.ServiceId, item.RegisterDate);
                total += (1 * item.Price);
            }
            return total;
        }

        public async Task<GetSellInvoicesResponse> Handle(GetSellInvoicesRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.GetSellInvoicesAsync();
            var inventoryItemHistories = await _inventoryItemHistoryRepository.GetAllAsync();
            var invoicePayments = await _invoiceRepository.GetAllInvoicePaymentsAsync();
            var invoiceInventoryItems = await _invoiceRepository.GetAllInvoiceInventoryItemsAsync();
            List<GetSellInvoices_InvoiceDTO> answer = new();
            int i = 0;
            foreach (var item in invoices.Where(a => a.Type == Domain.Enums.InvoiceType.Sell))
            {
                long paidAmount = invoicePayments.Where(b => b.InvoiceId == item.Id).Select(b => b.PaidAmount).Sum();
                float total = await this.CalculateInvoiceTotalCost(item.Id);
                answer.Add(new GetSellInvoices_InvoiceDTO
                (
                    InvoiceCode: item.Code.ToString(),
                    InvoiceDate: item.RegisterDate,
                    InvoiceDateString: item.RegisterDate.ToString(),
                    InvoiceId: item.Id,
                    RowNumber: i + 1,
                    InvoiceTotalPrice: total,
                    InvoicePaidAmount: paidAmount,
                    VehicleName: item.Vehicle?.Name,
                    CustomerName: item.VehicleId.HasValue ? (item.Vehicle.Customer.FirstName + " " + item.Vehicle.Customer.LastName) : (item.CustomerId.HasValue ? (item.Customer.FirstName + " " + item.Customer.LastName) : (""))
                ));
            }
            //var data = invoices.Where(a => a.Type == Domain.Enums.InvoiceType.Sell).Select(async (a, i) =>
            //{


            //    return new GetSellInvoices_InvoiceDTO
            //    (
            //        InvoiceCode: a.Code.ToString(),
            //        InvoiceDate: a.RegisterDate,
            //        InvoiceDateString: a.RegisterDate.ToString(),
            //        InvoiceId: a.Id,
            //        RowNumber: i + 1,
            //        InvoiceTotalPrice: total,
            //        InvoicePaidAmount: paidAmount,
            //        VehicleName: a.Vehicle?.Name,
            //        CustomerName: a.VehicleId.HasValue ? (a.Vehicle.Customer.FirstName + " " + a.Vehicle.Customer.LastName) : (a.CustomerId.HasValue ? (a.Customer.FirstName + " " + a.Customer.LastName) : (""))

            //    );
            //}).ToList();
            var response = new GetSellInvoicesResponse(Data:answer);
            return response;
        }
    }
}
