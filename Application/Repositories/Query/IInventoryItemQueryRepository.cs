using Application.ReadModels;

namespace Application.Repositories.Query;

public interface IInventoryItemQueryRepository
{
    Task<List<InventoryItemRM>> GetAsync();
    Task<List<InventoryItemRM>> GetByInventoryItemIdAsync(Guid inventoryItemId);
    Task AddAsync(InventoryItemRM inventoryItemRM);
    Task UpdateAsync(InventoryItemRM inventoryItemRM);
}
