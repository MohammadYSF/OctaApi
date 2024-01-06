using Command.Core.Domain.InventoryItem;

namespace Command.Core.Application.Repositories;
public interface IInventoryItemCommandRepository
{
    Task AddAsync(InventoryItemَAggregate inventoryItemAggregate);
    Task UpdateAsync(InventoryItemَAggregate inventoryItemAggregate);
    Task UpdateAsync(List<InventoryItemَAggregate> inventoryItemAggregates);
    Task<int> GenerateNewCodeAsync();
    Task<InventoryItemَAggregate?> GetByIdAsync(Guid id);
    Task<List<InventoryItemَAggregate>> GetByIdsAsync(List<Guid> ids);
    Task<InventoryItemَAggregate?> GetByCodeAsync(int code);
}
