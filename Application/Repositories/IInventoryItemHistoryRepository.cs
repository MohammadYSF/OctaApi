using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Repositories
{
    public interface IInventoryItemHistoryRepository
    {
        Task<List<InventoryItemHistory>> GetAllAsync();
        Task AddAsync(InventoryItemHistory entity);
        Task<InventoryItemHistory?> GetLatestByInventoryItemIdAndDateAsync(Guid inventoryItemId, DateTime dateTime);        
    }
}
