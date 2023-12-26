//using OctaApi.Application.Features.InvoiceFeatures.GetBuyInvoices;
//using OctaApi.Application.Features.InvoiceFeatures.GetDailySellInvoices;
//using OctaApi.Application.Features.InvoiceFeatures.GetInvoiceReportInfo;
//using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems;
//using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;
//using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
//using OctaApi.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OctaApi.Application.Repositories
//{
//    public interface IInvoiceRepository
//    {
//        Task<List<Invoice>> GetAllAsync();
//        Task<List<InvoicePayment>> GetAllInvoicePaymentsAsync();
//        Task<List<InvoiceInventoryItem>> GetAllInvoiceInventoryItemsAsync();
//        Task<InvoiceInventoryItem?> GetInvoiceInventoryItemById(Guid invoiceInventoryItemId);

//        Task<GetInvoiceReportInfoResponse> GetInvoiceReportInfoAsync(Guid invoiceId);
//        Task<List<Invoice>> GetDailySellInvoicesAsync();
//        Task<List<InvoiceService>> GetSellInvoicesServicesAsync(Guid invoiceId);
//        Task<List<InvoiceInventoryItem>> GetSellInvoiceInventoryItemsAsync(Guid invoiceId);
//        Task<List<Invoice>> GetSellInvoicesAsync(); 
//        //Task<List<GetBuyInvoices_InvoiceDTO>> GetBuyInvoicesAsync(); 
//        Task AddAsync(Invoice entity);
//        void Delete(Invoice entity);
//        void Update(Invoice entity);
//        Task<int> GetNewInvoiceCode();
//        Task<Invoice?> GetById(Guid id);
//        Task DeleteInvoiceInventoryItemsAsync(List<Guid> invoiceInventoryItemIds);
//        Task DeleteInvoiceServicesAsync(List<Guid> invoiceServicesIds);

//        Task AddInvoiceInventoryItemsAsync(List<InvoiceInventoryItem> invoiceInventoryItems);    
//        Task AddInvoiceServicesAsync(List<InvoiceService> invoiceServices);
//        Task AddInvoicePaymentsAsync(List<InvoicePayment> invoicePayments);
//        Task<List<InvoicePayment>> GetInvoicePaymentsByInvoiceIdAsync(Guid invoiceId);
//        Task<List<InvoiceService>> GetInvoiceServicesByInvoiceIdAsync(Guid invoiceId);
//        Task<List<InvoiceInventoryItem>> GetInvoiceInventoryItemsByInvoiceId(Guid invoiceId);
//    }
//}
