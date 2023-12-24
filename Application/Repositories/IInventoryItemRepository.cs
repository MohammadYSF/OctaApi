using OctaApi.Domain.InventoryItem;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Repositories
{
    public interface IInventoryItemRepository
    {
        Task<List<InventoryItem>> GetAllAsync();
        //Task AddAsync(InventoryItem entity);
        Task AddAsync(InventoryItemَAggregate inventoryItemAggregate);
        //void Update(InventoryItem entity);
        Task UpdateAsync(InventoryItemَAggregate inventoryItemAggregate);
        Task UpdateAsync(List<InventoryItemَAggregate> inventoryItemAggregates);
        void Delete(InventoryItem entity);
        //Task<int> GetNewCode();
        Task<int> GenerateNewCodeAsync();
        //Task<InventoryItem?> GetByIdAsync(Guid id);
        Task<InventoryItemَAggregate?> GetByIdAsync(Guid id);
        Task<List<InventoryItemَAggregate>> GetByIdsAsync(List<Guid> ids);
        //Task<InventoryItem?> GetByCodeAsync(int code);
        Task<InventoryItemَAggregate?> GetByCodeAsync(int code);



    }
}
