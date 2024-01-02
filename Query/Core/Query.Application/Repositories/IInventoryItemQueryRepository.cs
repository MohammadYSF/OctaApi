
using Query.Application.ReadModels;

namespace Query.Application.Repositories;

public interface IInventoryItemQueryRepository
{
    Task<List<InventoryItemRM>> GetAsync();
    Task<List<InventoryItemRM>> GetByInventoryItemIdAsync(Guid inventoryItemId);
    Task AddAsync(InventoryItemRM inventoryItemRM);
    Task UpdateAsync(InventoryItemRM inventoryItemRM);
    Task CheckCacheAsync();
}
