using Command.Core.Application.Repositories;
using Command.Core.Domain.InventoryItem;
using Command.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Command.Infrastructure.Persistence.Repositories;

public class InventoryItemCommandRepository : IInventoryItemCommandRepository
{
    private readonly WriteDbContext _writeDbContext;

    public InventoryItemCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task AddAsync(InventoryItemَAggregate inventoryItemAggregate)
    {
        await _writeDbContext.InventoryItems.AddAsync(inventoryItemAggregate);
    }

    public async Task<int> GenerateNewCodeAsync()
    {
        List<int> usedCodes = await _writeDbContext.InventoryItems.Select(a => a.Code.Value).ToListAsync();
        if (usedCodes.Count == 0) return 1;
        int min = usedCodes.Min();
        int max = usedCodes.Max();
        List<int> unUsedCodes = Enumerable.Range(min, max).Where(a => !usedCodes.Contains(a)).ToList();
        if (unUsedCodes.Count > 0) return unUsedCodes[0];
        return max + 1;
    }

    public async Task<InventoryItemَAggregate?> GetByCodeAsync(int code)
    {
        return await _writeDbContext.InventoryItems.FirstOrDefaultAsync(a => a.Code.Value == code);
    }

    public async Task<InventoryItemَAggregate?> GetByIdAsync(Guid id)
    {
        return await _writeDbContext.InventoryItems.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<InventoryItemَAggregate>> GetByIdsAsync(List<Guid> ids)
    {
        return await _writeDbContext.InventoryItems.Where(a => ids.Contains(a.Id)).ToListAsync();
    }

    public Task UpdateAsync(InventoryItemَAggregate inventoryItemAggregate)
    {
        _writeDbContext.InventoryItems.Update(inventoryItemAggregate);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(List<InventoryItemَAggregate> inventoryItemAggregates)
    {
        _writeDbContext.InventoryItems.UpdateRange(inventoryItemAggregates);
        return Task.CompletedTask;
    }
}
