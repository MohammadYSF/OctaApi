using Application.ReadModels;

namespace Application.Repositories.Query;

public interface IInventoryItemQueryRepository
{
    Task<List<InventoryItemRM>> GetAsync();
    Task AddAsync(InventoryItemRM inventoryItemRM);
}
