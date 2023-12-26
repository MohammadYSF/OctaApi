using Application.ReadModels;
using Microsoft.EntityFrameworkCore;
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

    public async Task<SellInvoiceRM?> GetBySellInvoiceIdAsync(Guid sellInvoiceId)
    {
        return await _queryDbContext.SellInvoiceRMs.FirstOrDefaultAsync(a => a.SellInvoiceId == sellInvoiceId);
    }

    public async Task<SellInvoiceInventoryItemRM?> GetSellInvoiceInventoryItemRMBySellInviceInventoryItemId(Guid sellInvoiceInventoryItemId)
    {
        return await _queryDbContext.SellInvoiceInventoryItemRMs.FirstOrDefaultAsync(a => a.Id == sellInvoiceInventoryItemId);
    }

    public async Task<SellInvoiceServiceRM?> GetSellInvoiceServiceRMBySellInvoicecServiceId(Guid sellInvoiceServiceId)
    {
        return await _queryDbContext.SellInvoiceServiceRMs.FirstOrDefaultAsync(a => a.Id == sellInvoiceServiceId);
    }

    public Task UpdateAsync(SellInvoiceRM sellInvoiceRM)
    {
        _queryDbContext.SellInvoiceRMs.Update(sellInvoiceRM);
        return Task.CompletedTask;
    }
}
