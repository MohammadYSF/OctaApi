using Microsoft.EntityFrameworkCore;
using Query.Application.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;
namespace Query.Persistence.Repositories;
public class SellInvoiceQueryRepository : ISellInvoiceQueryRepository
{
    private readonly QueryDbContext _queryDbContext;

    public SellInvoiceQueryRepository(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
    }

    public async Task AddAsync(SellInvoiceRM sellInvoiceRM)
    {
        await _queryDbContext.SellInvoiceRMs.AddAsync(sellInvoiceRM);
    }

    public async Task AddAsync(SellInvoiceServiceRM sellInvoiceServiceRM)
    {
        await _queryDbContext.SellInvoiceServiceRMs.AddAsync(sellInvoiceServiceRM);
    }

    public async Task AddAsync(SellInvoiceInventoryItemRM sellInvoiceInventoryRM)
    {
        await _queryDbContext.SellInvoiceInventoryItemRMs.AddAsync(sellInvoiceInventoryRM);
    }

    public Task AddAsync(SellInvoicePaymentRM sellInvoicePaymentRM)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(SellInvoiceInventoryItemRM sellInvoiceInventoryRM)
    {
        _queryDbContext.SellInvoiceInventoryItemRMs.Remove(sellInvoiceInventoryRM);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(SellInvoiceServiceRM sellInvoiceServiceRM)
    {
        _queryDbContext.SellInvoiceServiceRMs.Remove(sellInvoiceServiceRM);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(SellInvoiceRM sellInvoiceRM)
    {
        _queryDbContext.SellInvoiceRMs.Remove(sellInvoiceRM);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(List<SellInvoicePaymentRM> sellInvoicePaymentRMs)
    {
        throw new NotImplementedException();
    }

    public Task<List<SellInvoiceRM>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<SellInvoiceRM?> GetBySellInvoiceIdAsync(Guid sellInvoiceId)
    {
        return await _queryDbContext.SellInvoiceRMs.FirstOrDefaultAsync(a => a.SellInvoiceId == sellInvoiceId);
    }

    public Task<SellInvoiceDescriptionRM?> GetSellInvoiceDescriptionRMBySellInvoiceId(Guid sellInvoiceId)
    {
        throw new NotImplementedException();
    }

    public async Task<SellInvoiceInventoryItemRM?> GetSellInvoiceInventoryItemRMBySellInviceInventoryItemId(Guid sellInvoiceInventoryItemId)
    {
        return await _queryDbContext.SellInvoiceInventoryItemRMs.FirstOrDefaultAsync(a => a.Id == sellInvoiceInventoryItemId);
    }

    public Task<List<SellInvoiceInventoryItemRM>> GetSellInvoiceInventoryItemRMsBySellInvoiceId(Guid sellInvoiceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<SellInvoicePaymentRM>> GetSellInvoicePaymentRMsBySellInvoiceIdAsync(Guid sellInvoiceId)
    {
        throw new NotImplementedException();
    }

    public async Task<SellInvoiceServiceRM?> GetSellInvoiceServiceRMBySellInvoicecServiceId(Guid sellInvoiceServiceId)
    {
        return await _queryDbContext.SellInvoiceServiceRMs.FirstOrDefaultAsync(a => a.Id == sellInvoiceServiceId);
    }

    public Task<List<SellInvoiceServiceRM>> GetSellInvoiceServiceRMsBySellInvoiceId(Guid sellInvoiceId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SellInvoiceRM sellInvoiceRM)
    {
        _queryDbContext.SellInvoiceRMs.Update(sellInvoiceRM);
        return Task.CompletedTask;
    }
}
