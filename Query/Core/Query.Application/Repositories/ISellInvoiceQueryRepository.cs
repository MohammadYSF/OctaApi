using Application.ReadModels;

namespace Query.Application.Repositories;

public interface ISellInvoiceQueryRepository
{
    Task AddAsync(SellInvoiceRM sellInvoiceRM);
    Task UpdateAsync(SellInvoiceRM sellInvoiceRM);
    Task AddAsync(SellInvoiceServiceRM sellInvoiceServiceRM);
    Task AddAsync(SellInvoiceInventoryItemRM sellInvoiceInventoryRM);
    Task<SellInvoiceRM?> GetBySellInvoiceIdAsync(Guid sellInvoiceId);
    Task<SellInvoiceDescriptionRM?> GetSellInvoiceDescriptionRMBySellInvoiceId(Guid sellInvoiceId);
    Task<List<SellInvoiceInventoryItemRM>> GetSellInvoiceInventoryItemRMsBySellInvoiceId(Guid sellInvoiceId);
    Task<List<SellInvoiceServiceRM>> GetSellInvoiceServiceRMsBySellInvoiceId(Guid sellInvoiceId);
    Task<SellInvoiceServiceRM?> GetSellInvoiceServiceRMBySellInvoicecServiceId(Guid sellInvoiceServiceId);
    Task<SellInvoiceInventoryItemRM?> GetSellInvoiceInventoryItemRMBySellInviceInventoryItemId(Guid sellInvoiceInventoryItemId);
    Task DeleteAsync(SellInvoiceInventoryItemRM sellInvoiceInventoryRM);
    Task DeleteAsync(SellInvoiceServiceRM sellInvoiceServiceRM);
    Task DeleteAsync(SellInvoiceRM sellInvoiceRM);
    Task<List<SellInvoiceRM>> GetAsync();
}
