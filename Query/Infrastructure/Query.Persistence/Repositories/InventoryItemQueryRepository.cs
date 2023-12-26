using Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Query.Application.Repositories;
using Query.Persistence.Contexts;

namespace Query.Persistence.Repositories;
public class InventoryItemQueryRepository : IInventoryItemQueryRepository
{
    private QueryDbContext _queryDbContext;

    public InventoryItemQueryRepository(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
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
}
