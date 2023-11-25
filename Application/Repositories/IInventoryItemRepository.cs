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
        Task AddAsync(InventoryItem entity);
        void Update(InventoryItem entity);
        void Delete(InventoryItem entity);
        Task<int> GetNewCode();
        Task<InventoryItem?> GetByIdAsync(Guid id);
        Task<InventoryItem?> GetByCodeAsync(int code);



    }
}
