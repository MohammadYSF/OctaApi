using Microsoft.EntityFrameworkCore;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;

namespace Query.Persistence.Repositories;
public class InventoryItemQueryRepository : IInventoryItemQueryRepository
{
    private QueryDbContext _queryDbContext;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private readonly IDistributedCacheService<InventoryItemRM> _inventoryItemRMCache;

    public InventoryItemQueryRepository(QueryDbContext queryDbContext, IDistributedCacheService<InventoryItemRM> inventoryItemRMCache)
    {
        _queryDbContext = queryDbContext;
        _inventoryItemRMCache = inventoryItemRMCache;
    }

    public async Task AddAsync(InventoryItemRM inventoryItemRM)
    {
        await _queryDbContext.InventoryItemRMs.AddAsync(inventoryItemRM);
    }

    public async Task<List<InventoryItemRM>> GetAsync()
    {
        return await _queryDbContext.InventoryItemRMs.ToListAsync();
    }

    public async Task<List<InventoryItemRM>> GetByInventoryItemIdAsync(Guid inventoryItemId)
    {
        return await _queryDbContext.InventoryItemRMs.Where(a => a.InventoryItemId == inventoryItemId).ToListAsync();
    }

    public Task UpdateAsync(InventoryItemRM inventoryItemRM)
    {
        _queryDbContext.InventoryItemRMs.Update(inventoryItemRM);
        return Task.CompletedTask;
    }
    public async Task CheckCacheAsync()
    {
        if (_inventoryItemRMCache.Exists($"ids:{nameof(InventoryItemRM)}") == 0)
            await InitCacheAsync();
    }
    private async Task InitCacheAsync()
    {
        var exist = _inventoryItemRMCache.Exists($"ids:{nameof(InventoryItemRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.InventoryItemRMs.AsNoTracking().ToListAsync();
            _inventoryItemRMCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
    }
}
