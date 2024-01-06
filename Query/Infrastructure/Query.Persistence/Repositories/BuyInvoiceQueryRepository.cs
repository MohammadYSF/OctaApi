using Microsoft.EntityFrameworkCore;
using Query.Application.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;

namespace Query.Persistence.Repositories;

public class BuyInvoiceQueryRepository : IBuyInvoiceQueryRepository
{
    private readonly QueryDbContext _queryDbContext;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private readonly IDistributedCacheService<BuyInvoiceRM> _buyInvoiceRMCache;
    public BuyInvoiceQueryRepository(QueryDbContext queryDbContext, IDistributedCacheService<BuyInvoiceRM> buyInvoiceRMCache)
    {
        _queryDbContext = queryDbContext;
        _buyInvoiceRMCache = buyInvoiceRMCache;
    }
    public async Task CheckCacheAsync()
    {
        if (_buyInvoiceRMCache.Exists($"ids:{nameof(BuyInvoiceRM)}") == 0)
            await InitCacheAsync();
    }
    private async Task InitCacheAsync()
    {
        var exist = _buyInvoiceRMCache.Exists($"ids:{nameof(BuyInvoiceRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.BuyInvoiceRMs.AsNoTracking().ToListAsync();
            _buyInvoiceRMCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    public async Task AddAsync(BuyInvoiceRM buyInvoiceRM)
    {
        await _queryDbContext.BuyInvoiceRMs.AddAsync(buyInvoiceRM);
    }

    public async Task<List<BuyInvoiceRM>> GetAsync()
    {
        return await _queryDbContext.BuyInvoiceRMs.ToListAsync();
    }

    public async Task<BuyInvoiceRM?> GetByBuyInvoiceId(Guid buyInvoiceId)
    {
        return await _queryDbContext.BuyInvoiceRMs.FirstOrDefaultAsync(a => a.BuyInvoiceId == buyInvoiceId);
    }
}
