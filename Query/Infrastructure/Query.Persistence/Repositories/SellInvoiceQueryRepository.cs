using Microsoft.EntityFrameworkCore;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;
namespace Query.Persistence.Repositories;
public class SellInvoiceQueryRepository : ISellInvoiceQueryRepository
{
    private readonly QueryDbContext _queryDbContext;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private readonly IDistributedCacheService<SellInvoiceRM> _sellInvoiceCache;
    private readonly IDistributedCacheService<SellInvoiceInventoryItemRM> _sellInvoiceInventoryItemCache;
    private readonly IDistributedCacheService<SellInvoiceServiceRM> _sellInvoiceServiceCache;
    private readonly IDistributedCacheService<SellInvoiceDescriptionRM> _sellInvoiceDescriptionCache;
    public SellInvoiceQueryRepository(QueryDbContext queryDbContext, IDistributedCacheService<SellInvoiceRM> sellInvoiceCache, IDistributedCacheService<SellInvoiceInventoryItemRM> sellInvoiceInventoryItemCache, IDistributedCacheService<SellInvoiceServiceRM> sellInvoiceServiceCache, IDistributedCacheService<SellInvoiceDescriptionRM> sellInvoiceDescriptionCache)
    {
        _queryDbContext = queryDbContext;
        _sellInvoiceCache = sellInvoiceCache;
        _sellInvoiceInventoryItemCache = sellInvoiceInventoryItemCache;
        _sellInvoiceServiceCache = sellInvoiceServiceCache;
        _sellInvoiceDescriptionCache = sellInvoiceDescriptionCache;
    }
    public async Task CheckCacheAsync()
    {
        if (_sellInvoiceCache.Exists($"ids:{nameof(SellInvoiceRM)}") == 0)
            await InitCacheAsync1();
        if (_sellInvoiceInventoryItemCache.Exists($"ids:{nameof(SellInvoiceInventoryItemRM)}") == 0)
            await InitCacheAsync2();
        if (_sellInvoiceServiceCache.Exists($"ids:{nameof(SellInvoiceServiceRM)}") == 0)
            await InitCacheAsync3();
        if (_sellInvoiceDescriptionCache.Exists($"ids:{nameof(SellInvoiceDescriptionRM)}") == 0)
            await InitCacheAsync4();

    }
    private async Task InitCacheAsync1()
    {
        var exist = _sellInvoiceCache.Exists($"ids:{nameof(SellInvoiceRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.SellInvoiceRMs.AsNoTracking().ToListAsync();
            _sellInvoiceCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    private async Task InitCacheAsync4()
    {
        var exist = _sellInvoiceDescriptionCache.Exists($"ids:{nameof(SellInvoiceDescriptionRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.SellInvoiceDescriptionRMs.AsNoTracking().ToListAsync();
            _sellInvoiceDescriptionCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    private async Task InitCacheAsync3()
    {
        var exist = _sellInvoiceServiceCache.Exists($"ids:{nameof(SellInvoiceServiceRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.SellInvoiceServiceRMs.AsNoTracking().ToListAsync();
            _sellInvoiceServiceCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    private async Task InitCacheAsync2()
    {
        var exist = _sellInvoiceInventoryItemCache.Exists($"ids:{nameof(SellInvoiceInventoryItemRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.SellInvoiceInventoryItemRMs.AsNoTracking().ToListAsync();
            _sellInvoiceInventoryItemCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
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
